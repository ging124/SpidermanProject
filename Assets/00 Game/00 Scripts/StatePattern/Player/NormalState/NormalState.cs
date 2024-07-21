using Animancer;
using UnityEngine;

public class NormalState : BaseState
{
    [SerializeField] protected AnimancerLayer _normalBodyLayer;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer = _blackboard.animancer.Layers[0];
    }

    public override void UpdateState()
    {
        base.UpdateState();

        WallCheck();
        ZipPointCheck();

        if (_blackboard.onHit)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.hitState);
            return;
        }

        if(_blackboard.zipPoint != Vector3.zero && _blackboard.inputSO.buttonZip)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.startZipState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public void WallCheck()
    {
        _blackboard.wallFront = Physics.Raycast(new Vector3(_blackboard.playerController.transform.position.x, _blackboard.playerController.transform.position.y + _blackboard.character.GetHeight() / 2f, _blackboard.playerController.transform.position.z)
            , _blackboard.playerController.transform.forward, out _blackboard.frontWallHit, _blackboard.detectionLength, _blackboard.wallLayer);
    }

    public void ZipPointCheck()
    {
        if (Physics.SphereCast(_blackboard.cam.position, _blackboard.zipRange, _blackboard.cam.forward, out _blackboard.zipPointDetection, _blackboard.zipLength, _blackboard.wallLayer))
        {
            var wallScript = _blackboard.zipPointDetection.transform.GetComponent<WallScript>();
            _blackboard.zipPoint = wallScript.GetZipPoint(_blackboard.zipPointDetection.point);
        }
    }
}
