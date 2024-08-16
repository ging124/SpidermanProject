using Animancer;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : GroundState
{
    [SerializeField] private ClipTransition _dodgeLAnim;
    [SerializeField] private ClipTransition _dodgeRAnim;

    [SerializeField] private ClipTransition _dodgeBackAnim;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.character.useRootMotion = true;
        _blackboard.playerController.canAttack = false;
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

        if (_normalBodyLayer.CurrentState.NormalizedTime >= 0.6f)
        {
            _blackboard.playerController.canAttack = true;
        }

        if (_blackboard.inputSO.buttonDodge && _normalBodyLayer.CurrentState.NormalizedTime >= 0.6f)
        {
            _stateManager.ChangeState(_stateReferences.dodgeState);
            return StateStatus.Success;
        }

        if (_normalBodyLayer.CurrentState.NormalizedTime >= 1f)
        {
            _stateManager.ChangeState(_stateReferences.idleNormalState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.character.useRootMotion = false;
        base.ExitState();
    }

    public void Dodge()
    {
        _blackboard.playerController.transform.DOLookAt(_blackboard.playerController.transform.position + _blackboard.playerController.cam.transform.forward, 0.2f, AxisConstraint.Y);

        if (_blackboard.inputSO.move.y <= 0 && _blackboard.inputSO.move.x == 0)
        {
            _normalBodyLayer.Play(_dodgeBackAnim, 0.25f, FadeMode.FromStart);
        }
        else
        {
            Vector3 horizontal = _blackboard.playerController.cam.transform.right * _blackboard.inputSO.move.x;

            if (_blackboard.inputSO.move.x >= 0)
                _normalBodyLayer.Play(_dodgeRAnim, 0.25f, FadeMode.FromStart);
            else
                _normalBodyLayer.Play(_dodgeLAnim, 0.25f, FadeMode.FromStart);
        }
    }
}
