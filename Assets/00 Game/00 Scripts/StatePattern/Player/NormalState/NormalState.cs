using Animancer;
using UnityEngine;

public class NormalState : PlayerBaseState
{
    [SerializeField] protected AnimancerLayer _normalBodyLayer;

    public override void EnterState()
    {
        base.EnterState();
        _normalBodyLayer = _blackboard.playerController.animancer.Layers[0];
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        _blackboard.playerController.ZipPointCheck();
        _blackboard.playerController.TargetDetection();

        if (_blackboard.playerController.onSwim && _elapsedTime > 0.25 && _stateManager.currentState != _stateReferences.swimState)
        {
            _stateManager.ChangeState(_stateReferences.swimState);
            return StateStatus.Success;
        }

        if (_blackboard.playerController.zipPoint != Vector3.zero
            && _stateManager.currentState != _stateReferences.startZipState
            && _blackboard.inputSO.buttonZip)
        {
            _stateManager.ChangeState(_stateReferences.startZipState);
            return StateStatus.Success;
        }

        if(_stateManager.currentState != _stateReferences.dodgeState
            && _stateManager.currentState != _stateReferences.startGrabState 
            && _stateManager.currentState != _stateReferences.grabHitState
            && _stateManager.currentState != _stateReferences.ultimateAttackState)
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
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
