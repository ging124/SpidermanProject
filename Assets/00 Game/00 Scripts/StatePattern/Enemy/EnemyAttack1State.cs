using Animancer;
using UnityEngine;

public class EnemyAttack1State : EnemyAttackState
{
    [SerializeField] private ClipTransition[] _attack1Anim;

    public override void EnterState()
    {
        base.EnterState();
        _actionLayer.Play(_attack1Anim[Random.Range(0, _attack1Anim.Length - 1)], 0.25f, FadeMode.FromStart).Events.OnEnd = () =>
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
        };
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        return StateStatus.Running;
    }


    public override void ExitState()
    {
        base.ExitState();
    }
}
