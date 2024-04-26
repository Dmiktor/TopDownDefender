using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability-Dash-SimpleDash", menuName = "Player/Ability/Dash/SimpleDash", order = 1)]
public class SimpleDashAbility : PlayerAbility
{
    [SerializeField] private float dashSpeed;


    private Vector2 movementDirection;
    public override void Init(PlayerController playerController)
    {
        base.Init(playerController);
        player.PlayerInputValidator.OnDashTrigger += StartAbilityCasting;
        player.PlayerInputValidator.OnDashExitTrigger += CancelAbilityCasting;
        player.PlayerInputValidator.OnMousePosition += SetMovingVector;

    }
    public override void ExitAbility()
    {
        base.ExitAbility();
        player.PlayerInputValidator.OnDashTrigger -= StartAbilityCasting;
        player.PlayerInputValidator.OnDashExitTrigger -= CancelAbilityCasting;
        player.PlayerInputValidator.OnMousePosition -= SetMovingVector;
    }

    public override void StartAbilityCasting()
    {
        if (currentAbilityState == AbilityState.ReadyToUse)
        {
            castingCoroutine = player.StartCoroutine(AbilityCastings());
        }
    }
    public override void CancelAbilityCasting()
    {
        base.CancelAbilityCasting();
    }
    public override IEnumerator AbilityCastings()
    {
        currentAbilityState = AbilityState.InUse;
        player.PlayerMovement.IsBlocked = true;
        player.PlayerMovement.PlayerRigidBody.velocity = movementDirection * dashSpeed;
        yield return new WaitForSeconds(duration);
        player.PlayerMovement.IsBlocked = false;
        coolDownCoroutine = player.StartCoroutine(AbilityCooldown());
        castingCoroutine = null;
    }

    public override IEnumerator AbilityCooldown()
    {
        currentAbilityState = AbilityState.InCooldown;
        yield return new WaitForSeconds(cooldownTime);
        currentAbilityState = AbilityState.ReadyToUse;
        coolDownCoroutine = null;
    }
    private void SetMovingVector(Vector2 lookDirection)
    {
        this.movementDirection = (lookDirection - (Vector2)player.transform.position).normalized;
    }
}
