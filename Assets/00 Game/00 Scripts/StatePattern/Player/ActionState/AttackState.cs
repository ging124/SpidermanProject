using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : ActionState
{
    

    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _blackboard.character.useRootMotion = true;
        //blackboard.character.SetRotationMode(EasyCharacterMovement.RotationMode.OrientWithRootMotion);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        
    }

    public override void ExitState()
    {
        _actionLayer.StartFade(0, 0.1f);
        base.ExitState();
    }

    

}
