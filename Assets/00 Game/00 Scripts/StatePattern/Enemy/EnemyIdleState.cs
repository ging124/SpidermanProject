using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyMovementState
{
    [SerializeField] ClipTransition _idleAnim;
    [SerializeField] float _timeToMove;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_idleAnim);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        /*if (_elapsedTime > _timeToMove)
        {
            _stateManager.ChangeState(_stateReferences.enemyMoveState);
            return StateStatus.Success;
        }*/

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
