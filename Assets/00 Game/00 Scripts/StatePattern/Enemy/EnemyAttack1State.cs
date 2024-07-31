using Animancer;
using UnityEngine;

public class EnemyAttack1State : EnemyAttackState
{
    [SerializeField] private ClipTransition _attack1Anim;

    public override void EnterState()
    {
        base.EnterState();
        _actionLayer.Play(_attack1Anim);
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
            //_stateManager.ChangeState(_stateManager.stateReferences.enemyAttack2State);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
