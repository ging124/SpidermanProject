using Animancer;
using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHitState : EnemyNormalState
{
    [SerializeField] private ClipTransition _hitAnim;
    [SerializeField] private float _timeToIdle = 0.1f;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_hitAnim);
        TakeDamage();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (!_blackboard.enemyController.onHit && _elapsedTime > _timeToIdle)
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
            return StateStatus.Success;
        }

        if(_blackboard.enemyController.currentHP <= 0)
        {
            _stateManager.ChangeState(_stateReferences.enemyDeadState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public void TakeDamage()
    {
        /*_hitEffect.transform.right = Vector3.Lerp(_hitEffect.transform.right, _blackboard.enemyController.transform.position - _blackboard.enemyController.player.transform.position, rotateSpeed);
        _hitEffect.Play();*/
        _blackboard.enemyController.currentHP -= _blackboard.enemyController.hitDamage;
        _blackboard.uIEnemyBlackboard.enemyHPBar.EnemyHPChange(_blackboard.enemyController.currentHP, _blackboard.enemyController.enemyData.maxHP.Value);
        //var damageText = Instantiate(_blackboard.enemyController.enemyData.damageText, _rectTransform.position, Quaternion.identity ,_rectTransform);
        //damageText.GetComponent<DamageFloat>().damageText.text = _blackboard.enemyController.hitDamage.ToString();

        if (_blackboard.enemyController.currentHP <= 0)
        {
            _blackboard.enemyController.currentHP = 0;
        }

    }
}
