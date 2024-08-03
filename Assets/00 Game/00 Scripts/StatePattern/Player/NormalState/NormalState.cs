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

        _blackboard.playerController.WallCheck();
        _blackboard.playerController.ZipPointCheck();
        _blackboard.playerController.EnemyCheck();

        if (_blackboard.playerController.onSwim && _elapsedTime > 0.25 && _stateManager.currentState != _stateReferences.swimState)
        {
            _stateManager.ChangeState(_stateReferences.swimState);
            return StateStatus.Success;
        }

        if (_blackboard.playerController.zipPoint != Vector3.zero
            && _blackboard.playerController.zipLength <= _blackboard.playerController.maxZipLength && _blackboard.inputSO.buttonZip)
        {
            _stateManager.ChangeState(_stateReferences.startZipState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
