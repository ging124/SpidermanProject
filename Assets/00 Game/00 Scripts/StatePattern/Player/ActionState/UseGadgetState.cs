using Animancer;
using UnityEngine;

public class UseGadgetState : ActionState
{
    public override void EnterState()
    {
        base.EnterState();
        _blackboard.playerGadget.UseGadget();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_elapsedTime > 0.5f)
        {
            _stateManager.ChangeState(_stateReferences.normalActionState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _actionLayer.StartFade(0, 0.1f);
        base.ExitState();
    }
}
