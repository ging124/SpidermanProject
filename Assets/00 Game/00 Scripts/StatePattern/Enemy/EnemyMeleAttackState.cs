using Animancer;
using DG.Tweening;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMeleAttackState : EnemyAttackState
{
    [SerializeField] private ClipTransition[] _attack1Anim;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.enemyController.transform.DOLookAt(_blackboard.enemyController.target.transform.position, 0.2f, AxisConstraint.Y);
        _blackboard.enemyController.animancer.Play(_attack1Anim[Random.Range(0, _attack1Anim.Length)]);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.enemyController.animancer.States.Current.NormalizedTime >= 1)
        {
            _stateManager.ChangeState(_stateReferences.enemyRetreatState);
            return StateStatus.Success;
        }

        switch (_blackboard.enemyController.hitAttackType)
        {
            case AttackType.NormalAttack:
                _stateManager.ChangeState(_stateReferences.enemyHitState);
                return StateStatus.Success;
            case AttackType.HeavyAttack:
                _stateManager.ChangeState(_stateReferences.enemyKnockDownState);
                return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.enemyController.spiderSense.Stop();
        PlayerController player;
        if (_blackboard.enemyController.target.TryGetComponent<PlayerController>(out player))
        {
            player.haveEnemyTarget = false;
        }
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
