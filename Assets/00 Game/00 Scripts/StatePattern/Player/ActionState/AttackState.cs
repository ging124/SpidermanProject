 using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : ActionState
{
    public override void EnterState()
    {
        base.EnterState();
        _blackboard.playerController.zipIconImage.gameObject.SetActive(false);
        _blackboard.character.useRootMotion = true;
        _blackboard.character.SetRotationMode(RotationMode.OrientWithRootMotion);

        if(_blackboard.playerController.target != null)
        {
            Vector3 distance = _blackboard.playerController.target.transform.position - _blackboard.playerController.transform.position;
            Vector3 endValue = _blackboard.playerController.target.transform.position - distance * 0.1f;
            _blackboard.playerController.transform.DOLookAt(endValue, 0.2f, AxisConstraint.Y);

            if (distance.magnitude < 0.5f) _blackboard.character.useRootMotion = false;
        }
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (((StateManagerAction)_stateManager).stateManagerMovement.currentState == _stateReferences.dodgeState)
        {
            _stateReferences.meleAttackState.ResetCombo();
            _stateManager.ChangeState(_stateReferences.normalActionState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.character.useRootMotion = false;
        _blackboard.character.SetRotationMode(RotationMode.OrientToMovement);
        _actionLayer.StartFade(0, 0.1f);
        base.ExitState();
    }

    public void Attack(AttackType attackType)
    {
        if (_blackboard.playerController.target == null) return;
        else if (_blackboard.playerController.target != null 
            && Vector3.Distance(_blackboard.playerController.transform.position, _blackboard.playerController.target.transform.position) < _blackboard.playerController.mediumAttackRange + _blackboard.playerController.nearAttackRange)
        {
            _blackboard.playerController.playerData.levelSystem.GetExp(1);

            var target = _blackboard.playerController.target.GetComponent<IHitable>();
            var damage = _blackboard.playerController.playerData.RandomDamage(_blackboard.playerController.playerData.attackDamage);
            target.OnHit(damage, attackType);

            _blackboard.playerController.damagePrefab.Spawn(_blackboard.playerController.target.transform.position + Vector3.up, damage);
            var hitEffect = _blackboard.playerController.attackHitEffect;
            hitEffect.Spawn(_blackboard.playerController.target.transform.position, Quaternion.identity, null);
        }
    }

    public virtual void MoveToTarget()
    {
        Vector3 target = Vector3.MoveTowards(_blackboard.playerController.target.transform.position, _blackboard.playerController.transform.position, 0.7f);
        target.y = _blackboard.playerController.transform.position.y;
        _blackboard.playerController.transform.DOLookAt(_blackboard.playerController.target.transform.position, 0.2f, AxisConstraint.Y);
        _blackboard.playerController.transform.DOMove(target, 0.2f);
    }
}
