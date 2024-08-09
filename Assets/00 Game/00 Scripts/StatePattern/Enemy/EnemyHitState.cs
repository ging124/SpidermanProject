using Animancer;
using DG.Tweening;
using UnityEngine;

public class EnemyHitState : EnemyNormalState
{
    [SerializeField] private ClipTransition[] _hitAnimList;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.enemyController.agent.enabled = false;
        Debug.Log("Knockback");
        _normalBodyLayer.Play(_hitAnimList[Random.Range(0, _hitAnimList.Length - 1)], 0.25f, FadeMode.FromStart).Events.OnEnd = () =>
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
        };
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

        if (_blackboard.enemyController.enemyData.currentHp <= 0)
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

        _blackboard.enemyController.enemyData.currentHp -= _blackboard.enemyController.hitDamage;
        _blackboard.enemyController.uIEnemyBlackboard.enemyHPBar.EnemyHPChange(_blackboard.enemyController.enemyData.currentHp, _blackboard.enemyController.enemyData.maxHP.Value);

        if (_blackboard.enemyController.enemyData.currentHp <= 0)
        {
            _blackboard.enemyController.enemyData.currentHp = 0;
        }
    }
}
