using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack3State : AttackState
{
    [SerializeField] private ClipTransition _attack3Anim;

    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _actionLayer.Play(_attack3Anim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.inputSO.buttonAttack)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.meleAttackState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
