using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

public class AttackState : NormalState
{
    public override void EnterState()
    {
        base.EnterState();
        _blackboard.playerController.zipIconImage.gameObject.SetActive(false);

        _blackboard.character.useRootMotion = true;
        _blackboard.character.SetRotationMode(RotationMode.OrientWithRootMotion);

        if (_blackboard.playerController.target != null)
        {
            Vector3 lookAt = _blackboard.playerController.target.transform.position;
            lookAt.y = _blackboard.playerController.transform.position.y;
            _blackboard.playerController.transform.LookAt(lookAt);
        }
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.inputSO.buttonDodge)
        {
            _stateReferences.meleAttackState.ResetCombo();
            _stateManager.ChangeState(_stateReferences.dodgeState);
            return StateStatus.Success;
        }

        if (_blackboard.inputSO.buttonUltimate)
        {
            _stateReferences.meleAttackState.ResetCombo();
            _stateManager.ChangeState(_stateReferences.ultimateAttackState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.character.useRootMotion = false;
        _blackboard.character.SetRotationMode(RotationMode.OrientToMovement);
        base.ExitState();
    }

    public void Attack(AttackType attackType)
    {
        if (_blackboard.playerController.target == null) return;
        else if (_blackboard.playerController.target != null 
            && Vector3.Distance(_blackboard.playerController.transform.position, _blackboard.playerController.target.transform.position) < 2.5)
        {

            var target = _blackboard.playerController.target.GetComponent<IHitable>();
            var damage = RPGObject.RandomDamage(_blackboard.playerController.playerData.attackDamage);
            target.OnHit(damage, attackType);

            var hitEffect = _blackboard.playerController.attackHitEffect;
            Vector3 randomPos = new Vector3(Random.Range(0, 0.5f), Random.Range(0, 0.5f), 0);
            hitEffect.Spawn(_blackboard.playerController.target.transform.position + randomPos, Quaternion.identity, null);
        }
    }

    public virtual void MoveToTarget()
    {
        Vector3 target = Vector3.MoveTowards(_blackboard.playerController.target.transform.position, _blackboard.playerController.transform.position, 0.8f);
        target.y = _blackboard.playerController.transform.position.y;
        _blackboard.playerController.transform.DOLookAt(_blackboard.playerController.target.transform.position, 0.2f, AxisConstraint.Y);
        _blackboard.playerController.transform.DOMove(target, 0.4f);
    }
}
