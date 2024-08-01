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

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.enemyController.currentHP == _blackboard.enemyController.enemyData.maxHP.Value)
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

    IEnumerator PlayDeadEffect()
    {
        yield return new WaitForSeconds(1);
        _blackboard.enemyController.enemyDead.Raise();
        _blackboard.enemyController.enemyData.Despawn(_blackboard.enemyController.gameObject);
    }
}
