using Animancer;
using UnityEngine;

public class IdleActionState : ActionState
{
    [SerializeField] private float _timeToAttack;
    [SerializeField] private float _timeToChangeState;
    [SerializeField] private ClipTransition _idleActionAnim;


    public override void EnterState()
    {
        base.EnterState();
        _actionLayer.Play(_idleActionAnim);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_elapsedTime > _timeToChangeState || ((StateManagerAction)_stateManager).stateManagerMovement.currentState != _stateReferences.idleNormalState)
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
