using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack3State : AttackState
{
    [SerializeField] private ClipTransition _attack3Anim;
    [SerializeField] private float orbitDegreesPerSec;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _actionLayer.Play(_attack3Anim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.inputSO.buttonAttack && _elapsedTime > _timeChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.attack1State);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
