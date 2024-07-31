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

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.inputSO.buttonAttack
            && _blackboard.character.IsGrounded()
            && ((StateManagerAction)_stateManager).stateManagerMovement.currentState != _stateReferences.deadState
            && _elapsedTime > _timeToAttack)
        {
            _stateManager.ChangeState(_stateReferences.firstAttackState);
            return;
        }

        if (_elapsedTime > _timeToChangeState || ((StateManagerAction)_stateManager).stateManagerMovement.currentState != _stateReferences.idleNormalState)
        {
            _stateManager.ChangeState(_stateReferences.normalActionState);
            return;
        }
    }

    public override void ExitState()
    {
        _actionLayer.StartFade(0, 0.1f);
        base.ExitState();
    }
}
