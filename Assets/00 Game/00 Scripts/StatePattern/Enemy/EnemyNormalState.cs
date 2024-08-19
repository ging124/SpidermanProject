using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalState : EnemyBaseState
{
    [SerializeField] protected AnimancerLayer _normalBodyLayer;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer = _blackboard.enemyController.animancer.Layers[0];
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        _blackboard.enemyController.TargetDetection();
        CanAttack();

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public virtual void CanAttack()
    {
        if (_blackboard.enemyController.target == null)
        {
            _blackboard.enemyController.canAttack = false;
            return;
        }

        if (Vector3.Distance(_blackboard.enemyController.transform.position, _blackboard.enemyController.target.transform.position) <= _blackboard.enemyController.enemyData.attackRange)
        {
            _blackboard.enemyController.canAttack = true;
        }
        else
        {
            _blackboard.enemyController.canAttack = false;
        }
    }
/*
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_blackboard.enemyController.transform.position, _blackboard.enemyController.enemyData.dectectionRange);
    }*/
}
