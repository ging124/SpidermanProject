using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionState : EnemyBaseState
{
    [SerializeField] protected AnimancerLayer _actionLayer;
    [SerializeField] protected AvatarMask _actionMask;

    public override void EnterState(EnemyStateManager stateManager, EnemyBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _actionLayer = _blackboard.enemyController.animancer.Layers[1];
        _actionLayer.SetMask(_actionMask);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        //CanAttack();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    /*public virtual void CanAttack()
    {
        if (Physics.Raycast(_blackboard.enemyController.transform.position, _blackboard.enemyController.transform.forward, _blackboard.enemyController.enemyData.canAttackMaxDistance, _blackboard.enemyController.enemyData.playerLayer))
        {
            _blackboard.enemyController.canAttack = true;
        }
        else
        {
            _blackboard.enemyController.canAttack = false;
        }
    }*/

    /*void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(_blackboard.enemyController.transform.position, _blackboard.enemyController.transform.forward);
    }*/
}
