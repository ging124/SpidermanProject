using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionState : EnemyBaseState
{
    [SerializeField] protected AnimancerLayer _actionLayer;

    public override void EnterState()
    {
        base.EnterState();
        _actionLayer = _blackboard.enemyController.animancer.Layers[1];
    }

}
