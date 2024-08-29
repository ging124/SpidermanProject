using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateAttackState : NormalState
{
    [SerializeField] private Skill _ultimateSkill;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.character.SetMovementDirection(Vector3.zero);
        _ultimateSkill.UseSkill(_blackboard.playerController.transform, _blackboard.playerController.animancer, _blackboard.playerController.playerData.attackDamage);
    }
    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        MissAttack();

        if (_normalBodyLayer.CurrentState.NormalizedTime >= 1)
        {
            _stateManager.ChangeState(_stateReferences.idleNormalState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public void MissAttack()
    {
        if (_blackboard.playerController.hitDamage != 0)
        {
            _blackboard.playerController.missPrefab.Spawn(_blackboard.playerController.transform.position + Vector3.up, _blackboard.playerController.hitDamage);
        }
    }

}
