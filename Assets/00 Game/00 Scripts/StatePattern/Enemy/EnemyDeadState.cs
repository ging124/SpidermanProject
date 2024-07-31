using System.Collections;
using UnityEngine;

public class EnemyDeadState : EnemyNormalState
{

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.enemyController.capCollider.isTrigger = true;
        _blackboard.enemyController.rigid.isKinematic = true;
        StartCoroutine(PlayDeadEffect());
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.enemyController.currentHP == _blackboard.enemyController.enemyData.maxHP.Value)
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
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
        _blackboard.enemyController.enemyData.Despawn(_blackboard.enemyController.gameObject);
    }
}
