
using Pathfinding;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Follow-Path", menuName = "Enemy/States/Idle-States", order = 1)]
public class FollowPath : EnemySOBaseState
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float nextWaypointDistance = 2f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    public override void Init(BaseEnemy baseEnemy)
    {
        base.Init(baseEnemy);
    }
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
        enemy.ChaseTrigger.OnTarget += ChangeToChaseState;
    }

    private void ChangeToChaseState(Collider2D collider2D)
    {
        enemy.Target = collider2D.gameObject;
        enemy.EnemyStateMachine.ChangeState(enemy.ChaseState);
    }

    public override void ExitState()
    {
        base.ExitState();
        SceneController.Instance.EnemyManager.OnPathTick -= CalculatePath;
        enemy.ChaseTrigger.OnTarget -= ChangeToChaseState;

    }
    public override void ResetState()
    {
        base.ResetState();
        reachedEndOfPath = false;
        currentWaypoint = 0;
    }

    public virtual void CalculatePath()
    {
        enemy.Seeker.StartPath(enemy.EnemyRigidBody.position, SceneController.Instance.EnemyManager.AiTargetTransform.position, OnPathComplete);
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
