using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyMovementState
{
    [SerializeField] ClipTransition _idleAnim;
    [SerializeField] float _timeToMove;

    public override void EnterState(EnemyStateManager stateManager, EnemyBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_idleAnim);
        _timeToMove = Random.Range(5f, 10f);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        /*if (_elapsedTime > _timeToMove)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.enemyMoveState);
            return;
        }*/
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
