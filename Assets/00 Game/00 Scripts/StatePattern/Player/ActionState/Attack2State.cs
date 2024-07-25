using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2State : AttackState
{
    [SerializeField] private ClipTransition _attack2Anim;

    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _actionLayer.Play(_attack2Anim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_blackboard.inputSO.buttonAttack)
        {
        }

    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
