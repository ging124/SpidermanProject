using Animancer;
using UnityEngine;

public class EndGrabState : HitStatusState
{
    [SerializeField] private ClipTransition _endGrabAnim;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.character.useRootMotion = true;
        TakeDamage();
        _normalBodyLayer.Play(_endGrabAnim);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_normalBodyLayer.CurrentState.NormalizedTime >= 1)
        {
            _stateManager.ChangeState(_stateReferences.standUpState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.character.useRootMotion = false;
        _blackboard.playerController.canAttack = true;
        base.ExitState();
    }
}
