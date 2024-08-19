using Animancer;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyAimState : EnemyAttackState
{
    [SerializeField] private ClipTransition _aimAnim;
    [SerializeField] private Vector3 _aimLockPos;
    [SerializeField] private float _aimLockTime;
    [SerializeField] private float _time = 0;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.enemyController.animancer.Play(_aimAnim);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.enemyController.target == null)
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
            return StateStatus.Success;
        }
        else
        {
            if (_aimLockPos.x - _blackboard.enemyController.target.transform.position.x < 0.01f
            && _aimLockPos.z - _blackboard.enemyController.target.transform.position.z < 0.01f)
            {
                _time += Time.deltaTime;
                if (_time > _aimLockTime)
                {
                    _stateManager.ChangeState(_stateReferences.enemyRangeAttackState);
                    return StateStatus.Success;
                }
            }
            else
            {
                LineRenderer lineRenderer = ((RangeEnemyController)_blackboard.enemyController).lineRenderer;
                _time = 0;
            }

            Aim();
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _time = 0;
        LineRenderer lineRenderer = ((RangeEnemyController)_blackboard.enemyController).lineRenderer;
        lineRenderer.positionCount = 0;
        base.ExitState();
    }

    public void Aim()
    {
        LineRenderer lineRenderer = ((RangeEnemyController)_blackboard.enemyController).lineRenderer;
        lineRenderer.positionCount = 2;
        Transform gun = ((RangeEnemyController)_blackboard.enemyController).gunTransform;
        lineRenderer.SetPosition(0, gun.position);
        lineRenderer.SetPosition(1, gun.position + _blackboard.enemyController.transform.forward * 15);

        _aimLockPos = _blackboard.enemyController.target.transform.position;

        _aimLockPos.y = _blackboard.enemyController.transform.position.y;

        _blackboard.enemyController.transform.LookAt(_aimLockPos);
    }
}
