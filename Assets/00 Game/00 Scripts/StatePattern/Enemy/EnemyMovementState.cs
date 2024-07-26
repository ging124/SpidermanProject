using UnityEngine;

public class EnemyMovementState : EnemyNormalState
{
    public override void EnterState(EnemyStateManager stateManager, EnemyBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.enemyController.onHit)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.enemyHitState);
            return;
        }

        if (_blackboard.enemyController.followPlayer == true)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.enemyRunState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

   /* public void PlayerDetection()
    {
        Collider[] playerDetection = Physics.OverlapSphere(_blackboard.enemyController.transform.position, _blackboard.enemyController.enemyData.dectectionRange, _blackboard.enemyController.enemyData.playerLayer);
        if (playerDetection.Length != 0)
        {
            _blackboard.enemyController.FollowPlayer();
        }
        else
        {
            _blackboard.enemyController.followPlayer = false;
        }
    }*/
}
