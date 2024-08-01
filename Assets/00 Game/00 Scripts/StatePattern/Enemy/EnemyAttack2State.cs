using Animancer;
using UnityEngine;

public class EnemyAttack2State : EnemyAttackState
{
    [SerializeField] private ClipTransition _attack2Anim;

    public override void EnterState()
    {
        base.EnterState();
        _actionLayer.Play(_attack2Anim);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.enemyController.canAttack && _elapsedTime > _timeChangeState)
        {
            _stateManager.ChangeState(_stateReferences.enemyAttack1State);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
