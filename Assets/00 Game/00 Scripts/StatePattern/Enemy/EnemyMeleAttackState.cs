using Animancer;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMeleAttackState : EnemyAttackState
{
    [SerializeField] private ClipTransition[] _attack1Anim;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.enemyController.transform.DOLookAt(_blackboard.enemyController.target.transform.position, 0.2f, AxisConstraint.Y);
        _blackboard.enemyController.animancer.Play(_attack1Anim[Random.Range(0, _attack1Anim.Length)]).Events.OnEnd = () =>
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

    public void NormalAttack()
    {
        if(_blackboard.enemyController.enemyData.GetType() == typeof(Boss))
        {
            Attack(AttackType.HeavyAttack);
        }
        else
        {
            Attack(AttackType.NormalAttack);
        }
    }
}
