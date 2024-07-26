using Animancer;
using UnityEngine;

public class EnemyAttack2State : EnemyAttackState
{
    [SerializeField] private ClipTransition _attack2Anim;

    public override void EnterState(EnemyStateManager stateManager, EnemyBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _actionLayer.Play(_attack2Anim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        if (_blackboard.enemyController.canAttack && _elapsedTime > _timeChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.enemyAttack1State);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
