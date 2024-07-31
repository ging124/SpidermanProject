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

    public override void UpdateState()
    {

        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

}
