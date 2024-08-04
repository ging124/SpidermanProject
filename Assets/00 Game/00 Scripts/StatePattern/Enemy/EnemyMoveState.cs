using Animancer;
using UnityEngine;

public class EnemyMoveState : EnemyCanMoveState
{
    [SerializeField] ClipTransition _enemyMoveAnim;
    private float _timeToIdle;


    public override void EnterState()
    {
        base.EnterState();
        _blackboard.enemyController.agent.enabled = false;
        _normalBodyLayer.Play(_enemyMoveAnim);
        _timeToIdle = Random.Range(1f, 5f);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        Movement(_blackboard.enemyController.enemyData.moveSpeed.Value);

        if (_elapsedTime > _timeToIdle)
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.enemyController.agent.enabled = true;
        base.ExitState();
    }
}
