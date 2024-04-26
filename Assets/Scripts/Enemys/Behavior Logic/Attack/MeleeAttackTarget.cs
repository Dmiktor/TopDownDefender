using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-MeleeAttack-Target", menuName = "Enemy/States/Attack-States", order = 1)]
public class MeleeAttackTarget : EnemySOBaseState
{
    [SerializeField] private float attackPreparation;
    [SerializeField] private float attackPostDelay;
    [SerializeField] private int attackDamage;
    private bool targetOutOfRange = false;
    private bool targetOutOfSightRange = false;
    private SimpleHealth targetHealth;

    public override void EnterState()
    {
        base.EnterState();
        enemy.StartCoroutine(AttackCoroutine());
        enemy.AttackTrigger.OnTarget += targetInAttackRange;
        enemy.AttackTrigger.OnTargetExit += targetOutAttackRange;
        enemy.ChaseTrigger.OnTarget += targetInChaseRange;
        enemy.ChaseTrigger.OnTargetExit += targetOutChaseRange;
        targetHealth = enemy.Target.GetComponent<SimpleHealth>();
    }
    private void targetInAttackRange(Collider2D d)
    {
        targetOutOfRange = false;
    }
    private void targetOutAttackRange(Collider2D d)
    {
        targetOutOfRange = true;
    }
    private void targetInChaseRange(Collider2D d)
    {
        targetOutOfSightRange = false;
    }
    private void targetOutChaseRange(Collider2D d)
    {
        targetOutOfSightRange = true;
    }
    private IEnumerator AttackCoroutine()
    {
        enemy.EnemyRigidBody.velocity = Vector3.zero;
        yield return new WaitForSeconds(attackPreparation);
        if (!targetOutOfRange )
        {
            targetHealth.TakeDamage(attackDamage);
        }
        yield return new WaitForSeconds(attackPostDelay);
        DecideNextMove();
    }

    private void DecideNextMove()
    {
        if (!targetOutOfRange)
        {
            enemy.StartCoroutine(AttackCoroutine());
        }
        else
        {
            if (targetOutOfSightRange)
            {
                enemy.EnemyStateMachine.ChangeState(enemy.IdleState);
                enemy.Target = null;
            }
            else
            {
                enemy.EnemyStateMachine.ChangeState(enemy.ChaseState);
            }
        }
    }
    public override void ResetState()
    {
        targetOutOfRange = false;
        targetOutOfSightRange = false;
        targetHealth = null;
    }
    public override void ExitState()
    {
        base.ExitState();
        enemy.AttackTrigger.OnTarget -= targetInAttackRange;
        enemy.AttackTrigger.OnTargetExit -= targetOutAttackRange;
        enemy.AttackTrigger.OnTarget -= targetInChaseRange;
        enemy.AttackTrigger.OnTargetExit -= targetOutChaseRange;

    }
}
