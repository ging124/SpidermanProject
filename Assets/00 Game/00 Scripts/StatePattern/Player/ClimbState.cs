using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClimbState : CanMoveState
{
    [SerializeField] private LinearMixerTransition _climbBlendTree;
    [SerializeField] private float _timeToIdle;
    public float climbSpeed;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _blackboard.playerController.transform.DORotate(Quaternion.LookRotation(Vector3.up, -_blackboard.playerController.transform.forward).eulerAngles, 0.2f);
        _normalBodyLayer.Play(_climbBlendTree);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Movement();
        _climbBlendTree.State.Parameter = Mathf.Lerp(_climbBlendTree.State.Parameter, _blackboard.character.GetSpeed(), 55 * Time.deltaTime);

        if (!_blackboard.character.IsGrounded() && _elapsedTime > _timeToIdle)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleNormalState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    protected override void Movement()
    {
        GetInput();

        _blackboard.character.SetMovementDirection(_blackboard.movement);
    }

    protected override void GetInput()
    {
        Vector2 input = _blackboard.inputSO.move;
        Vector3 horizontal = new Vector3(input.x, 0, 0);
        Vector3 vertical = new Vector3(0, input.y, 0);
        _blackboard.movement = (vertical + horizontal);
    }

}
