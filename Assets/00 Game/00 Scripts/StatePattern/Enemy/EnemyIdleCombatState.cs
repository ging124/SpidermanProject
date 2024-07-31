using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleCombatState : EnemyMovementState
{
    [SerializeField] ClipTransition _idleCombatAnim;
    [SerializeField] float _timeToIdle;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_idleCombatAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_elapsedTime > _timeToIdle)
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
