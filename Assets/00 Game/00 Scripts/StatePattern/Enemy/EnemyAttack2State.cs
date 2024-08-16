using Animancer;
using UnityEngine;

public class EnemyAttack2State : EnemyAttackState
{
    [SerializeField] private ClipTransition _attack2Anim;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_attack2Anim);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.enemyController.canAttack && _normalBodyLayer.CurrentState.NormalizedTime >= 1)
        {
            _stateManager.ChangeState(_stateReferences.enemyAttackState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
