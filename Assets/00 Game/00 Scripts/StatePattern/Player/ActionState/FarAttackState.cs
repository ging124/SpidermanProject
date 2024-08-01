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

        _actionLayer.Play(_farHit.hitAnim).Events.OnEnd = () =>
        {
            _stateManager.ChangeState(_stateReferences.idleActionState);
        };
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
        _blackboard.playerController.SetLineRenderer(_rightLineRenderer, _blackboard.playerController.rightHand, _blackboard.playerController.enemyTarget.transform.position);
    }

}
