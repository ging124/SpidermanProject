using Animancer;
using UnityEngine;

public class NormalState : BaseState
{
    [SerializeField] protected AnimancerLayer _normalBodyLayer;

    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer = _blackboard.playerController.animancer.Layers[0];
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _blackboard.playerController.WallCheck();
        _blackboard.playerController.ZipPointCheck();
        _blackboard.playerController.EnemyCheck();

        if (_blackboard.playerController.onHit)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.hitState);
            return;
        }

        if (_blackboard.playerController.onSwim && _elapsedTime > 0.25 && _stateManager.currentState != _stateManager.stateReferences.swimState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.swimState);
            return;
        }

        if (_blackboard.playerController.zipPoint != Vector3.zero && _blackboard.playerController.zipLength <= _blackboard.playerController.maxZipLength && _blackboard.inputSO.buttonZip)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.startZipState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
