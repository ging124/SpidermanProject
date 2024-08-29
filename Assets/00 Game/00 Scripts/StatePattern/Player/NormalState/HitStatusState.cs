using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStatusState : NormalState
{
    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.playerController.currentHP <= 0)
        {
            _stateManager.ChangeState(_stateReferences.deadState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public void TakeDamage()
    {
        _blackboard.cameraShake.ShakeCamera(0.75f, 0.1f);
        int damageValue = _blackboard.playerController.hitDamage;
        _blackboard.playerController.currentHP -= damageValue;
        _blackboard.playerController.damagePrefab.Spawn(_blackboard.playerController.transform.position + Vector3.up, damageValue);
        _blackboard.playerController.playerChangeHP.Invoke(_blackboard.playerController.currentHP, _blackboard.playerController.playerData.maxHP);

        if (_blackboard.playerController.currentHP <= 0)
        {
            _blackboard.playerController.currentHP = 0;
        }
    }
}
