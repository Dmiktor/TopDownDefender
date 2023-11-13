using System;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemySOBaseState CurrentEnemyState { get; set; }

    public void Init(EnemySOBaseState startingState)
    {
        CurrentEnemyState = startingState;
        startingState.EnterState();
    }

    public void ChangeState(EnemySOBaseState newState)
    {
        CurrentEnemyState.ExitState();
        CurrentEnemyState = newState;
        CurrentEnemyState.EnterState();
    }

    internal void DoFixedUpdate()
    {
        CurrentEnemyState.DoFixedUpdate();
    }

    internal void DoUpdate()
    {
        CurrentEnemyState.DoUpdate();

    }
    public void ExitState()
    {
        CurrentEnemyState?.ExitState();
    }
}
