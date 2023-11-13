using Pathfinding;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private SimpleHealth enemyHealth;
    [SerializeField] private Rigidbody2D enemyRigidBody;
    [SerializeField] private EnemySOBaseState idleState;
    [SerializeField] private EnemySOBaseState chaseState;
    [SerializeField] private EnemySOBaseState attackState;
    [SerializeField] private Seeker seeker;
    [SerializeField] private InRangeTrigger chaseTrigger;
    [SerializeField] private InRangeTrigger attackTrigger;

    private EnemyStateMachine enemyStateMachine;

    public EnemySOBaseState IdleState { get; set; }
    public EnemySOBaseState ChaseState { get; set; }
    public EnemySOBaseState AttackState { get; set; }
    public EnemyStateMachine EnemyStateMachine => enemyStateMachine;
    public SimpleHealth EnemyHealth => enemyHealth;
    public Rigidbody2D EnemyRigidBody => enemyRigidBody;
    public Seeker Seeker => seeker;
    public InRangeTrigger ChaseTrigger  => chaseTrigger;
    public InRangeTrigger AttackTrigger => attackTrigger;

    public GameObject Target { get; internal set; }

    private void Awake()
    {
        IdleState = Instantiate(idleState);
        ChaseState = Instantiate(chaseState);
        AttackState = Instantiate(attackState);

        enemyStateMachine = new EnemyStateMachine();
    }

    private void Start()
    {
        enemyHealth.OnDeath += Die;

        IdleState.Init(this);
        ChaseState.Init(this);
        AttackState.Init(this);
        enemyStateMachine.Init(IdleState);
    }
    private void Update()
    {
        enemyStateMachine.DoUpdate();
    }

    private void FixedUpdate()
    {
        enemyStateMachine.DoFixedUpdate();
    }
    private void Die()
    {
        enemyStateMachine.ExitState();
        Destroy(IdleState);
        Destroy(ChaseState);
        Destroy(AttackState);
        enemyHealth.OnDeath -= Die;
        Destroy(gameObject);
    }
}
