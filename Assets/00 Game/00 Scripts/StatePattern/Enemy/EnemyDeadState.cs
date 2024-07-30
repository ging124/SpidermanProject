using System.Collections;
using Animancer;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeadState : EnemyNormalState
{

    public override void EnterState(EnemyStateManager stateManager, EnemyBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _blackboard.enemyController.capCollider.isTrigger = true;
        _blackboard.enemyController.rigid.isKinematic = true;
        StartCoroutine(PlayDeadEffect());
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(_blackboard.enemyController.currentHP == _blackboard.enemyController.enemyData.maxHP.Value)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.enemyIdleState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    IEnumerator PlayDeadEffect()
    {
        yield return new WaitForSeconds(1);
        _blackboard.enemyController.enemyDead.Raise();
        _blackboard.enemyController.updateQuestProgress.Raise(_blackboard.enemyController.enemyData);
        _blackboard.enemyController.enemyData.Despawn(_blackboard.enemyController.gameObject);
    }
}
