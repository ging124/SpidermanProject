using Animancer;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemyMoveState : EnemyCanMoveState
{
    [SerializeField] ClipTransition _enemyMoveAnim;
    private float _timeToIdle;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_enemyMoveAnim);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if(_elapsedTime > _timeToIdle)
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
