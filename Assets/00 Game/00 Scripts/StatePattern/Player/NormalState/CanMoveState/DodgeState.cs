using Animancer;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : NormalState
{
    [SerializeField] private ClipTransition _dodgeAnim;
    [SerializeField] private ClipTransition _dodgeBackAnim;

    [SerializeField] private float _timeToIdle;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.character.SetMovementDirection(Vector3.zero);
        Dodge();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.inputSO.buttonDodge && _elapsedTime > _timeToIdle)
        {
            _stateManager.ChangeState(_stateReferences.dodgeState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public void Dodge()
    {
        if (_blackboard.inputSO.move.y <= 0 && _blackboard.inputSO.move.x == 0)
        {
            _normalBodyLayer.Play(_dodgeBackAnim, 0.25f, FadeMode.FromStart).Events.OnEnd = () =>
            {
                _stateManager.ChangeState(_stateReferences.idleNormalState);

            };
            _blackboard.playerController.transform.DOMove(_blackboard.playerController.transform.position - _blackboard.playerController.transform.forward, 0.2f);
        }
        else
        {
            Vector2 input = _blackboard.inputSO.move;
            Vector3 horizontal = _blackboard.playerController.cam.transform.right * input.x;
            _normalBodyLayer.Play(_dodgeAnim, 0.25f, FadeMode.FromStart).Events.OnEnd = () =>
            {
                _stateManager.ChangeState(_stateReferences.idleNormalState);

            };
            _blackboard.playerController.transform.DOMove(_blackboard.playerController.transform.position + horizontal, 0.2f);
        }
    }
}
