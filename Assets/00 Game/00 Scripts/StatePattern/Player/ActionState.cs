using Animancer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActionState : BaseState
{
    [SerializeField] protected AnimancerLayer _actionLayer;
    [SerializeField] protected AvatarMask _actionMask;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _actionLayer = _blackboard.animancer.Layers[1];
        _actionLayer.SetMask(_actionMask);
    }

    public override void UpdateState()
    {

        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

}
