using Animancer;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttackState : EnemyAttackState
{
    [SerializeField] private ClipTransition _rangeAttackAnim;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.enemyController.transform.DOLookAt(_blackboard.enemyController.target.transform.position, 0.2f, AxisConstraint.Y);
        _blackboard.enemyController.animancer.Play(_rangeAttackAnim).Events.OnEnd = () =>
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
        };
        Shoot();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        PlayerController player;
        if (_blackboard.enemyController.target.TryGetComponent<PlayerController>(out player))
        {
            player.haveEnemyTarget = false;
        }
        base.ExitState();
    }

    public void Shoot()
    {
        var bulletObject = ((RangeEnemyController)_blackboard.enemyController).enemyBullet.Spawn(((RangeEnemyController)_blackboard.enemyController).gunTransform.position, _blackboard.enemyController.transform.rotation);
        var bullet = bulletObject.GetComponent<EnemyBulletController>();
        bullet.Shoot();
    }

}
