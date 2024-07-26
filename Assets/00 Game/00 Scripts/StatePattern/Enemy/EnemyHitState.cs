using Animancer;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHitState : EnemyNormalState
{
    [SerializeField] private ClipTransition _hitAnim;
    [SerializeField] private float _timeToIdle = 0.1f;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private float rotateSpeed = 100;
    [SerializeField] private RectTransform _rectTransform;

    public override void EnterState(EnemyStateManager stateManager, EnemyBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_hitAnim);
        TakeDamage();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!_blackboard.enemyController.onHit && _elapsedTime > _timeToIdle)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.enemyIdleActionState);
        }

        if(_blackboard.enemyController.enemyData.currentHP <= 0)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.enemyDeadState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public void TakeDamage()
    {
        _hitEffect.transform.right = Vector3.Lerp(_hitEffect.transform.right, _blackboard.enemyController.transform.position - _blackboard.enemyController.player.transform.position, rotateSpeed);
        _hitEffect.Play();
        _blackboard.enemyController.enemyData.currentHP -= _blackboard.enemyController.hitDamage;
        var damageText = Instantiate(_blackboard.enemyController.enemyData.damageText, _rectTransform.position, Quaternion.identity ,_rectTransform);
        damageText.GetComponent<DamageFloat>().damageText.text = _blackboard.enemyController.hitDamage.ToString();

        if (_blackboard.enemyController.enemyData.currentHP <= 0)
        {
            _blackboard.enemyController.enemyData.currentHP = 0;
        }

    }
}
