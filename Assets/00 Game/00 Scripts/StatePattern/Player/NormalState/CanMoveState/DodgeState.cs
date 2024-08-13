using Animancer;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : NormalState
{
    [SerializeField] private ClipTransition _dodgeLAnim;
    [SerializeField] private ClipTransition _dodgeRAnim;

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

        if (_normalBodyLayer.CurrentState.NormalizedTime > 0.6f)
        {
            _stateManager.ChangeState(_stateReferences.idleNormalState);
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
            _blackboard.playerController.transform.DOMove(_blackboard.playerController.transform.position - _blackboard.playerController.transform.forward * 1.3f, 0.2f);
            _normalBodyLayer.Play(_dodgeBackAnim, 0.25f, FadeMode.FromStart);
        }
        else
        {
            Vector2 input = _blackboard.inputSO.move;
            Vector3 horizontal = _blackboard.playerController.cam.transform.right * input.x;
            _blackboard.playerController.transform.DOMove(_blackboard.playerController.transform.position + horizontal * 1.3f, 0.2f);
            if (input.x >= 0)
            {
                _normalBodyLayer.Play(_dodgeRAnim, 0.25f, FadeMode.FromStart);
            }
            else
            {
                _normalBodyLayer.Play(_dodgeLAnim, 0.25f, FadeMode.FromStart);
            }
        }
    }
}
