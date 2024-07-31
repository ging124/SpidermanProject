using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : BaseState
{
    [SerializeField] protected EnemyBlackboard _blackboard;
    [SerializeField] protected EnemyStateReferences _stateReferences;

    public override void InitState(StateManager stateManager, Blackboard blackboard, StateReferences stateReferences)
    {
        _stateManager = stateManager;
        _blackboard = blackboard as EnemyBlackboard;
        _stateReferences = stateReferences as EnemyStateReferences;
    }
}
