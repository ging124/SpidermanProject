using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : EnemyCanMoveState
{
    [SerializeField] ClipTransition _enemyRunAnim;

    public override void EnterState(EnemyStateManager stateManager, EnemyBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_enemyRunAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.enemyController.followPlayer == false)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.enemyIdleState);
            _blackboard.enemyController.agent.SetDestination(_blackboard.enemyController.transform.position);
            return;
        }
        else
        {
            _blackboard.enemyController.movement = _blackboard.enemyController.player.transform.position - _blackboard.enemyController.transform.position;
            Movement(_blackboard.enemyController.player.transform.position);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
