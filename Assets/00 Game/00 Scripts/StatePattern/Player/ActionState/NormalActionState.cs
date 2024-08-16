using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalActionState : ActionState
{

    public override void EnterState()
    {
        base.EnterState();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }
        

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
