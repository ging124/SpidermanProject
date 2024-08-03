using Animancer;
using DG.Tweening;
using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHitState : EnemyNormalState
{
    [SerializeField] private ClipTransition _hitAnim;
    [SerializeField] private float _timeToIdle = 0.1f;
    [SerializeField] AnimancerState _state;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer.Play(_hitAnim, 0.25f, FadeMode.FromStart);
        TakeDamage();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.enemyController.onHit && _elapsedTime > 0.1f)
        {
            _stateManager.ChangeState(_stateReferences.enemyHitState);
            return StateStatus.Success;
        }

        if (!_blackboard.enemyController.onHit && _elapsedTime > _timeToIdle)
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
            return StateStatus.Success;
        }

        if (_blackboard.enemyController.currentHP <= 0)
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
        Vector3 target = _blackboard.enemyController.player.transform.position;
        target.y = _blackboard.enemyController.transform.position.y;
        Vector3 knockBackDirection = _blackboard.enemyController.transform.position - target;

        _blackboard.enemyController.transform.DOLookAt(target, 0.1f);
        _blackboard.enemyController.transform.DOMove(_blackboard.enemyController.transform.position + knockBackDirection.normalized * 1.25f, 0.2f);

        /*_hitEffect.transform.right = Vector3.Lerp(_hitEffect.transform.right, _blackboard.enemyController.transform.position - _blackboard.enemyController.player.transform.position, rotateSpeed);
        _hitEffect.Play();*/
        _blackboard.enemyController.currentHP -= _blackboard.enemyController.hitDamage;
        _blackboard.enemyController.uIEnemyBlackboard.enemyHPBar.EnemyHPChange(_blackboard.enemyController.currentHP, _blackboard.enemyController.enemyData.maxHP.Value);
        //var damageText = Instantiate(_blackboard.enemyController.enemyData.damageText, _rectTransform.position, Quaternion.identity ,_rectTransform);
        //damageText.GetComponent<DamageFloat>().damageText.text = _blackboard.enemyController.hitDamage.ToString();

        if (_blackboard.enemyController.currentHP <= 0)
        {
            _blackboard.enemyController.currentHP = 0;
        }
    }
}
