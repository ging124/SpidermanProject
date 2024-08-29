using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimState : NormalState
{
    [SerializeField] private LinearMixerTransition _swimAnim;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_swimAnim);
        _blackboard.character.SetVelocity(Vector3.zero);
        _blackboard.character.Swim();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        Movement();
        _swimAnim.State.Parameter = Mathf.Lerp(_swimAnim.State.Parameter, _blackboard.character.GetSpeed() * _blackboard.character.GetMovementDirection().magnitude, 40 * Time.deltaTime);

        if (_blackboard.inputSO.buttonJump && _elapsedTime > 0.25)
        {
            _blackboard.character.LaunchCharacter((_blackboard.playerController.transform.up + _blackboard.playerController.transform.forward) * 30, true);
            _stateManager.ChangeState(_stateReferences.fallState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.character.StopSwimming();
        base.ExitState();
    }

    protected virtual void Movement()
    {
        GetInput();

        _blackboard.character.SetMovementDirection(_blackboard.playerController.movement.normalized);
    }

    protected virtual void GetInput()
    {
        Vector2 input = _blackboard.inputSO.move;
        Vector3 vertical = _blackboard.playerController.cam.transform.forward * input.y;
        Vector3 horizontal = _blackboard.playerController.cam.transform.right * input.x;
        _blackboard.playerController.movement = (vertical + horizontal);
        _blackboard.playerController.movement.y = 0;
    }
}
