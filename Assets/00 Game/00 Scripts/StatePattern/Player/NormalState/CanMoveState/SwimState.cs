using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimState : CanMoveState
{
    [SerializeField] private LinearMixerTransition _swimAnim;

    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_swimAnim);
        _blackboard.character.SetVelocity(Vector3.zero);
        _blackboard.character.Swim();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        Movement();
        _swimAnim.State.Parameter = Mathf.Lerp(_swimAnim.State.Parameter, _blackboard.character.GetSpeed() * _blackboard.character.GetMovementDirection().magnitude, 40 * Time.deltaTime);

        if (_blackboard.inputSO.buttonJump && _elapsedTime > 0.25)
        {
            _blackboard.character.LaunchCharacter(_blackboard.playerController.transform.up * 40, true);
            _stateManager.ChangeState(_stateManager.stateReferences.onAirState);
            return;
        }
    }

    public override void ExitState()
    {
        _blackboard.character.StopSwimming();
        base.ExitState();
    }
}
