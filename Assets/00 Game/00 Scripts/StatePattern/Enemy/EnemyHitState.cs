using Animancer;
using DG.Tweening;
using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHitState : EnemyNormalState
{
    [SerializeField] private ClipTransition _hitAnim;
    [SerializeField] private float _timeToIdle = 0.1f;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.enemyController.agent.enabled = false;
        Debug.Log("Knockback");
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
        _blackboard.enemyController.agent.enabled = true;
        base.ExitState();
    }

    public void TakeDamage()
    {
        _blackboard.enemyController.transform.LookAt(new Vector3(_blackboard.enemyController.player.transform.position.x, transform.position.y, _blackboard.enemyController.player.transform.position.z));
        _blackboard.enemyController.transform.DOMove(_blackboard.enemyController.transform.position - (_blackboard.enemyController.transform.forward / 2), 0.2f).SetDelay(0.1f);

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
