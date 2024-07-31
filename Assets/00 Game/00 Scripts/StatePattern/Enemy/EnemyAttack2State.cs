using Animancer;
using UnityEngine;

public class EnemyAttack2State : EnemyAttackState
{
    [SerializeField] private ClipTransition _attack2Anim;

    public override void EnterState()
    {
        base.EnterState();
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
            _stateManager.ChangeState(_stateReferences.enemyAttack1State);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
