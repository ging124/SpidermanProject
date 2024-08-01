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

    public override void ExitState()
    {
        base.ExitState();
    }
}
