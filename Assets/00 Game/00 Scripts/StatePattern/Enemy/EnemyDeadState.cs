using Animancer;
using System;
using System.Collections;
using UnityEngine;

public class EnemyDeadState : EnemyNormalState
{
    [SerializeField] private ClipTransition deadAnim;
    public override void EnterState()
    {
        base.EnterState();
        _blackboard.enemyController.canHit = false;
        _blackboard.enemyController.animancer.Animator.applyRootMotion = true;
        _blackboard.enemyController.rigid.isKinematic = true;
        _normalBodyLayer.Play(deadAnim);
        StartCoroutine(PlayDeadEffect());
    }

    public override StateStatus UpdateState()
    {

        if (_blackboard.enemyController.currentHP == _blackboard.enemyController.enemyData.maxHP)
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.enemyController.canHit = true;
        _blackboard.enemyController.animancer.Animator.applyRootMotion = false;
        base.ExitState();
    }

    IEnumerator PlayDeadEffect()
    {
        yield return new WaitForSeconds(1);
        _blackboard.enemyController.enemyDead.Raise();
        _blackboard.enemyController.enemyData.Despawn(_blackboard.enemyController.gameObject);
    }
}

