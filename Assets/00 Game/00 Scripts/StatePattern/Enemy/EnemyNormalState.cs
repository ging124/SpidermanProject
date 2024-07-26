using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalState : EnemyBaseState
{
    [SerializeField] protected AnimancerLayer _normalBodyLayer;

    public override void EnterState(EnemyStateManager stateManager, EnemyBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer = _blackboard.enemyController.animancer.Layers[0];
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
