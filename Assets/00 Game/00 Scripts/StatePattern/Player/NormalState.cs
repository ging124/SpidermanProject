using Animancer;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

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
        Debug.DrawRay(new Vector3(_blackboard.playerController.transform.position.x, _blackboard.playerController.transform.position.y + _blackboard.character.GetHeight() / 2f, _blackboard.playerController.transform.position.z)
            ,_blackboard.playerController.transform.forward * _blackboard.detectionLength, Color.red);

        Debug.DrawRay(new Vector3(_blackboard.playerController.transform.position.x, _blackboard.playerController.transform.position.y + _blackboard.character.GetHeight(), _blackboard.playerController.transform.position.z)
            - _blackboard.playerController.transform.up / 2f
            , _blackboard.playerController.transform.forward * _blackboard.detectionLength, Color.red);

        if (_blackboard.onHit)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.hitState);
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

        _blackboard.wallInHead = Physics.Raycast(new Vector3(_blackboard.playerController.transform.position.x, _blackboard.playerController.transform.position.y + _blackboard.character.GetHeight(), _blackboard.playerController.transform.position.z)
            - _blackboard.playerController.transform.up/2f
            , _blackboard.playerController.transform.forward, out _blackboard.frontWallHit, _blackboard.detectionLength, _blackboard.wallLayer);
    }
}
