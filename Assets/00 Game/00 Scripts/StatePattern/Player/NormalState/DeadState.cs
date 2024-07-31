using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : NormalState
{
    [SerializeField] private ClipTransition _deadAnim;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_deadAnim);
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
