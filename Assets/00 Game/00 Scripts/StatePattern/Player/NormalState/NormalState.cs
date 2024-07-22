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

        if(_blackboard.zipPoint != Vector3.zero && _blackboard.zipLength <= _blackboard.maxZipLength && _blackboard.inputSO.buttonZip)
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
        if (Physics.SphereCast(_blackboard.cam.position, _blackboard.zipDetectionRange, _blackboard.cam.forward, out _blackboard.zipPointDetection, _blackboard.zipDetectionLength, _blackboard.wallLayer))
        {
            var wallScript = _blackboard.zipPointDetection.transform.GetComponent<WallScript>();
            _blackboard.zipPoint = wallScript.GetZipPoint(_blackboard.zipPointDetection.point);
        }
        else
        {
            _blackboard.zipPoint = Vector3.zero;
        }
        
        if(_blackboard.zipPoint != Vector3.zero && _blackboard.zipLength < _blackboard.maxZipLength)
        {
            _blackboard.zipIconImage.gameObject.SetActive(true);
            Camera camera = _blackboard.cam.GetComponent<Camera>();
            _blackboard.zipIconImage.transform.position = camera.WorldToScreenPoint(_blackboard.zipPoint);
        }
        else
        {
            _blackboard.zipIconImage.gameObject.SetActive(false);
        }
    }
}
