using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : BaseState
{
    [SerializeField] protected PlayerBlackboard _blackboard;
    [SerializeField] protected PlayerStateReferences _stateReferences;

    public override void InitState(StateManager stateManager, Blackboard blackboard, StateReferences stateReferences)
    {
        _stateManager = stateManager;
        _blackboard = blackboard as PlayerBlackboard;
        _stateReferences = stateReferences as PlayerStateReferences;
    }
}
