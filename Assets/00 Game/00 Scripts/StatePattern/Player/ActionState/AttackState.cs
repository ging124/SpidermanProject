using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : ActionState
{
    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _blackboard.character.useRootMotion = true;
        _blackboard.character.SetRotationMode(RotationMode.OrientWithRootMotion);

        if(_blackboard.playerController.enemyTarget != null)
        {
            Vector3 distance = _blackboard.playerController.enemyTarget.transform.position - _blackboard.playerController.transform.position;
            Vector3 endValue = _blackboard.playerController.enemyTarget.transform.position - distance * 0.1f;
            _blackboard.playerController.transform.DOLookAt(endValue, 0.2f, AxisConstraint.Y);
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        _blackboard.character.useRootMotion = false;
        _blackboard.character.SetRotationMode(RotationMode.OrientToMovement);
        _actionLayer.StartFade(0, 0.1f);
        base.ExitState();
    }
}
