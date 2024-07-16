using Animancer;
using UnityEngine;

public class MoveState : MovementState
{
    [SerializeField] private LinearMixerTransition _moveBlendTree;
    [SerializeField] private float _timeToRun = 0.2f;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_moveBlendTree);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        Movement();

        _moveBlendTree.State.Parameter = Mathf.Lerp(_moveBlendTree.State.Parameter, _blackboard.character.GetSpeed(), 55 * Time.deltaTime);

        if (_blackboard.inputSO.buttonJump && _blackboard.character.IsGrounded() || !_blackboard.character.IsGrounded())
        {
            _stateManager.ChangeState(_stateManager.stateReferences.startJumpState);
            return;
        }

        if (_blackboard.inputSO.move == Vector2.zero)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.stopMoveState);
            return;
        }

        if (_blackboard.character.GetSpeed() >= 6 && _elapsedTime > _timeToRun)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.runState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
