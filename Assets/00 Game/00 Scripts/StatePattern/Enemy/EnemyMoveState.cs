using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyCanMoveState
{
    [SerializeField] ClipTransition _enemyMoveAnim;
    private float _timeToIdle;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_enemyMoveAnim);
        _timeToIdle = Random.Range(1f, 5f);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Movement(_blackboard.enemyController.enemyData.moveSpeed.Value);

        if(_elapsedTime > _timeToIdle)
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
