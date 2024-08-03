using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionState : EnemyBaseState
{
    [SerializeField] protected AnimancerLayer _actionLayer;
    [SerializeField] protected AvatarMask _actionMask;

    public override void EnterState()
    {
        base.EnterState();
        _actionLayer = _blackboard.enemyController.animancer.Layers[1];
        _actionLayer.SetMask(_actionMask);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        CanAttack();

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public virtual void CanAttack()
    {
        if (Physics.Raycast(_blackboard.enemyController.transform.position, _blackboard.enemyController.transform.forward, _blackboard.enemyController.enemyData.canAttackMaxDistance, _blackboard.enemyController.enemyData.playerLayer))
        {
            _blackboard.enemyController.canAttack = true;
        }
        else
        {
            _blackboard.enemyController.canAttack = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(_blackboard.enemyController.transform.position, _blackboard.enemyController.transform.forward);
    }
}
