using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability-Attack-MeleeAttack", menuName = "Player/Ability/Attack/MeleeAttack", order = 1)]
public class MeleeAttack : PlayerAbility
{
    [SerializeField] private float attackReach;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask layerMask;
    
    private bool attackKeyDown = false;
    private Vector2 attackPosition;

    public override void Init(PlayerController playerController)
    {
        base.Init(playerController);
        playerController.PlayerInputValidator.OnAttackTrigger += StartAbilityCasting;
        playerController.PlayerInputValidator.OnAttackExitTrigger += CancelAbilityCasting;
        playerController.PlayerInputValidator.OnMousePosition += SetAttackPosition;
        currentAbilityState = AbilityState.ReadyToUse;
    }
    public override void ExitAbility()
    {
        base.ExitAbility();
        player.PlayerInputValidator.OnAttackTrigger -= StartAbilityCasting;
        player.PlayerInputValidator.OnAttackExitTrigger -= CancelAbilityCasting;
        player.PlayerInputValidator.OnMousePosition -= SetAttackPosition;
    }
    public override void StartAbilityCasting()
    {
        if (currentAbilityState == AbilityState.ReadyToUse)
        {
            currentAbilityState = AbilityState.InUse;
            castingCoroutine = player.StartCoroutine(AbilityCastings());
            attackKeyDown = true;
        }
        else if (currentAbilityState == AbilityState.InCooldown)
        {
            attackKeyDown = true;
        }
    }
    public override void CancelAbilityCasting()
    {
        attackKeyDown = false;
    }
    public override IEnumerator AbilityCastings()
    {
        yield return new WaitForSeconds(duration);
        DoAttack();
        castingCoroutine = null;
        currentAbilityState = AbilityState.InCooldown;
        coolDownCoroutine = player.StartCoroutine(AbilityCooldown());
    }

    public override IEnumerator AbilityCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        coolDownCoroutine = null;
        if (attackKeyDown)
        {
            currentAbilityState = AbilityState.InUse;
            castingCoroutine = player.StartCoroutine(AbilityCastings());
        }
        else
        {
            currentAbilityState = AbilityState.ReadyToUse;
        }
    }
    private void SetAttackPosition(Vector2 lookPosition)
    {
        attackPosition = (Vector2)player.transform.position + ((lookPosition - (Vector2)player.transform.position).normalized * attackReach);
    }
    private void DoAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPosition, attackRange, layerMask);

        Debug.Log(hits);

        foreach(Collider2D hit in hits)
        {
            if (hit.gameObject.TryGetComponent<IDamageable>(out IDamageable hitTarget))
            {
                hitTarget.TakeDamage(damage);
            }
           
        }
    }

}
