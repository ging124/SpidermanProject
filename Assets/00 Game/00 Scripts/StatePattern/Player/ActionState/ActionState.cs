using Animancer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : PlayerBaseState
{
    [SerializeField] protected AnimancerLayer _actionLayer;
    [SerializeField] protected AvatarMask _actionMask;

    public override void EnterState()
    {
        base.EnterState();
        _actionLayer = _blackboard.playerController.animancer.Layers[1];
        _actionLayer.SetMask(_actionMask);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (((StateManagerAction)_stateManager).stateManagerMovement.currentState == _stateReferences.deadState)
        {
            _stateManager.ChangeState(_stateReferences.normalActionState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }
}
