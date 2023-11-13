using Pathfinding;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Chase-Chase-Target", menuName = "Enemy/States/Chase-States", order = 1)]
public class ChaseTarget : EnemySOBaseState
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float nextWaypointDistance = 2f;
    [SerializeField] private float lostDelay = 1.5f;
    
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Coroutine lostTargetCoroutine;

    public override void DoFixedUpdate()
    {
        base.DoFixedUpdate();
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - enemy.EnemyRigidBody.position).normalized;
        if (enemy.EnemyRigidBody.velocity.magnitude < maxSpeed)
        {
            enemy.EnemyRigidBody.AddForce(direction * speed);

        }
        if (nextWaypointDistance > Vector2.SqrMagnitude(enemy.EnemyRigidBody.position - (Vector2)path.vectorPath[currentWaypoint]))
        {
            currentWaypoint++;
        }

    }

    public override void DoUpdate()
    {
        base.DoUpdate();
    }

    public override void EnterState()
    {
        base.EnterState();
        CalculatePath();
        SceneController.Instance.EnemyManager.OnPathTick += CalculatePath;
        enemy.ChaseTrigger.OnTargetExit += ForgetAboutTarget;
        enemy.ChaseTrigger.OnTarget += RememberTarget;
        enemy.AttackTrigger.OnTarget += ChangeToAttackState;
    }

    private void ChangeToAttackState(Collider2D targetCollider)
    {
        enemy.EnemyStateMachine.ChangeState(enemy.AttackState);
    }
    private void ForgetAboutTarget(Collider2D targetCollider)
    {
        lostTargetCoroutine = enemy.StartCoroutine(LostTargetCoroutine());
    }
    private IEnumerator LostTargetCoroutine()
    {
        yield return new WaitForSeconds(lostDelay);
        enemy.Target = null;
        enemy.EnemyStateMachine.ChangeState(enemy.IdleState);
    }
    private void RememberTarget(Collider2D targetCollider)
    {
        if (lostTargetCoroutine != null) 
        {
            enemy.StopCoroutine(lostTargetCoroutine);
            lostTargetCoroutine = null;
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        SceneController.Instance.EnemyManager.OnPathTick -= CalculatePath;
        enemy.ChaseTrigger.OnTargetExit -= ForgetAboutTarget;
        enemy.ChaseTrigger.OnTarget -= RememberTarget;
        enemy.AttackTrigger.OnTarget -= ChangeToAttackState;

    }
    public override void ResetState()
    {
        base.ResetState();
        reachedEndOfPath = false;
        currentWaypoint = 0;
        lostTargetCoroutine = null;
    }

    public virtual void CalculatePath()
    {
        enemy.Seeker.StartPath(enemy.EnemyRigidBody.position, enemy.Target.transform.position, OnPathComplete);
    }

    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            this.path = path;
            currentWaypoint = 0;
        }
    }
}

