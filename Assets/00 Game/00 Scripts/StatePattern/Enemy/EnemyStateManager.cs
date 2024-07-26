using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public EnemyBaseState startState;
    public EnemyBaseState currentState;

    public EnemyStateReferences stateReferences;
    public EnemyBlackboard enemyBlackboard;

    public virtual void Start()
    {
        currentState = startState;
        currentState?.EnterState(this, enemyBlackboard);
    }

    public virtual void ChangeState(EnemyBaseState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState(this, enemyBlackboard);
    }

    public virtual void Update()
    {
        currentState.UpdateState();
    }
}
