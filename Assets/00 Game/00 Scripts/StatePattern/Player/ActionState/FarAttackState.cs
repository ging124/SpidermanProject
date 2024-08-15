using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarAttackState : AttackState
{
    [SerializeField] private ClipTransition _zipAnim;
    [SerializeField] private Hit _farHit;
    [SerializeField] private LineRenderer _rightLineRenderer;

    public override void EnterState()
    {
        base.EnterState();

        _actionLayer.Play(_zipAnim);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.inputSO.buttonAttack && _elapsedTime > _farHit.timeNextAttack)
        {
            _stateManager.ChangeState(_stateReferences.meleAttackState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public void Zip()
    {
        _rightLineRenderer.positionCount = 2;
        _blackboard.playerController.SetLineRenderer(_rightLineRenderer, _blackboard.playerModel.rightHand, _blackboard.playerController.target.transform.position);
    }

    public override void MoveToTarget()
    {
        _rightLineRenderer.positionCount = 0;
        base.MoveToTarget();
        _actionLayer.Play(_farHit.hitAnim).Events.OnEnd = () =>
        {
            _stateManager.ChangeState(_stateReferences.idleActionState);
        };
    }

    public void FarAttack()
    {
        Attack(_farHit.attackType);
    }
}
