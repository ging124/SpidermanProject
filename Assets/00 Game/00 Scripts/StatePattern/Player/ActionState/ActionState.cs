using Animancer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : PlayerBaseState
{
    [SerializeField] protected AnimancerLayer _actionLayer;
    [SerializeField] protected AvatarMask _actionMask;

    public override void EnterState()
    {
        base.EnterState();
        _actionLayer = _blackboard.playerController.animancer.Layers[1];
        _actionLayer.SetMask(_actionMask);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.playerGadget.currentGadget.GetType() == typeof(HealingDrone))
        {
            if (_blackboard.inputSO.buttonGadget && _stateManager.currentState != _stateReferences.useGadgetState
            && !_blackboard.playerGadget.onUseGadget)
            {
                _stateManager.ChangeState(_stateReferences.useGadgetState);
                return StateStatus.Success;
            }
        }
        else
        {
            if (_blackboard.inputSO.buttonGadget && _blackboard.character.IsGrounded()
            && _stateManager.currentState != _stateReferences.useGadgetState
            && !_blackboard.playerGadget.onUseGadget)
            {
                _stateManager.ChangeState(_stateReferences.useGadgetState);
                return StateStatus.Success;
            }
        }

        /*if (_stateManager.currentState != _stateReferences.dodgeState
            && _stateManager.currentState != _stateReferences.startGrabState
            && _stateManager.currentState != _stateReferences.grabHitState)
        {
            switch (_blackboard.playerController.hitAttackType)
            {
                case AttackType.NormalAttack:
                    _stateManager.ChangeState(_stateReferences.staggerState);
                    return StateStatus.Success;
                case AttackType.HeavyAttack:
                    _stateManager.ChangeState(_stateReferences.knockDownState);
                    return StateStatus.Success;
                case AttackType.StartGrabAttack:
                    _stateManager.ChangeState(_stateReferences.startGrabState);
                    return StateStatus.Success;
            }
        }*/

        return StateStatus.Running;
    }
}
