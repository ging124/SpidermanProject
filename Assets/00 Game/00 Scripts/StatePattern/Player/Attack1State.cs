using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1State : AttackState
{
    [SerializeField] private ClipTransition _attack1Anim;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _actionLayer.Play(_attack1Anim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.inputSO.buttonAttack && _elapsedTime > _timeChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.attack2State);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
