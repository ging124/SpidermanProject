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

        WallCheck();
        ZipPointCheck();

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

    public void WallCheck()
    {
        _blackboard.playerController.wallFront = Physics.Raycast(new Vector3(_blackboard.playerController.transform.position.x, _blackboard.playerController.transform.position.y + _blackboard.character.GetHeight() / 2f, _blackboard.playerController.transform.position.z)
            , _blackboard.playerController.transform.forward, out _blackboard.playerController.frontWallHit, _blackboard.playerController.detectionLength, _blackboard.playerController.wallLayer);
    }

    public void ZipPointCheck()
    {
        if (Physics.SphereCast(_blackboard.playerController.cam.position, _blackboard.playerController.zipDetectionRange, _blackboard.playerController.cam.forward, out _blackboard.playerController.zipPointDetection, _blackboard.playerController.zipDetectionLength, _blackboard.playerController.wallLayer))
        {
            var wallScript = _blackboard.playerController.zipPointDetection.transform.GetComponent<WallScript>();
            _blackboard.playerController.zipPoint = wallScript.GetZipPoint(_blackboard.playerController.zipPointDetection.point);
        }
        else
        {
            _blackboard.playerController.zipPoint = Vector3.zero;
        }
        
        if(_blackboard.playerController.zipPoint != Vector3.zero && _blackboard.playerController.zipLength < _blackboard.playerController.maxZipLength)
        {
            _blackboard.playerController.zipIconImage.gameObject.SetActive(true);
            Camera camera = _blackboard.playerController.cam.GetComponent<Camera>();
            _blackboard.playerController.zipIconImage.transform.position = camera.WorldToScreenPoint(_blackboard.playerController.zipPoint);
        }
        else
        {
            _blackboard.playerController.zipIconImage.gameObject.SetActive(false);
        }
    }
}
