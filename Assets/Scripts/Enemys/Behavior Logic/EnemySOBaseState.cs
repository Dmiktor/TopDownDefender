using UnityEngine;

public class EnemySOBaseState : ScriptableObject
{
    protected BaseEnemy enemy;

    public virtual void Init(BaseEnemy enemy)
    {
        this.enemy = enemy;
    }

    public virtual void EnterState()
    {

    }
    public virtual void ExitState()
    {
        ResetState();
    }
    public virtual void DoUpdate()
    {

    }
    public virtual void DoFixedUpdate() 
    {
    }
    public virtual void ResetState()
    {
    }
}
