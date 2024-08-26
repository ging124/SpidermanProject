using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillState : EnemyNormalState
{
    public Skill[] listSkill;
    [SerializeField] private int _skillIndex;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.enemyController.transform.DOLookAt(_blackboard.enemyController.target.transform.position, 0.2f, AxisConstraint.Y);
        listSkill[_skillIndex].UseSkill(_blackboard.enemyController.transform, _blackboard.enemyController.animancer, _blackboard.enemyController.target, _blackboard.enemyController.enemyData.attackDamage);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        MissAttack();

        if (_blackboard.enemyController.animancer.States.Current.NormalizedTime >= 1)
        {
            _stateManager.ChangeState(_stateReferences.enemyRetreatState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public void ChoseSkill(int skillIndex)
    {
        this._skillIndex = skillIndex;
    }

    public void MissAttack()
    {
        if (_blackboard.enemyController.hitDamage != 0)
        {
            _blackboard.enemyController.missPrefab.Spawn(_blackboard.enemyController.transform.position + Vector3.up, _blackboard.enemyController.hitDamage);
        }
    }
}
