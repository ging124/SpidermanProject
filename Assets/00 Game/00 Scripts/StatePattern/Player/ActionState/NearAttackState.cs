using DG.Tweening;
using UnityEngine;

public class NearAttackState : AttackState
{
    [SerializeField] private Hit nearHit;

    public override void EnterState()
    {
        base.EnterState();

        _actionLayer.Play(nearHit.hitAnim).Events.OnEnd = () =>
        {
            _stateManager.ChangeState(_stateReferences.idleActionState);
        };
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.inputSO.buttonAttack && _elapsedTime > nearHit.timeNextAttack)
        {
            _stateManager.ChangeState(_stateReferences.meleAttackState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
