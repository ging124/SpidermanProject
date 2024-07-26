using System.Collections;
using Animancer;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeadState : EnemyNormalState
{
    [SerializeField] ClipTransition _enemyDeadAnim;

    public override void EnterState(EnemyStateManager stateManager, EnemyBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_enemyDeadAnim);
        _blackboard.enemyController.capCollider.isTrigger = true;
        _blackboard.enemyController.rigid.isKinematic = true;
        StartCoroutine(PlayDeadEffect());
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(_blackboard.enemyController.enemyData.currentHP == _blackboard.enemyController.enemyData.maxHP)
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
        _blackboard.enemyController.enemyData.Despawn(_blackboard.enemyController.gameObject);
    }
}
