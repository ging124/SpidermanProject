using Animancer;
using DG.Tweening;
using UnityEngine;

public class StartZipState : NormalState
{
    [SerializeField] private ClipTransition _zipAnim;
    [SerializeField] private float _timeToChangeState;
    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;
    [SerializeField] private Transform _leftHandTransform;
    [SerializeField] private Transform _rightHandTransform;


    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_zipAnim);
    }


    public override void UpdateState()
    {
        base.UpdateState();

        if (_stateManager.currentState != this)
        {
            return;
        }

        if (_elapsedTime > _timeToChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleZipState);
        }

        if (_blackboard.inputSO.buttonJump && _elapsedTime > _timeToChangeState - 0.2f)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.zipJumpState);
        }
    }

    public override void ExitState()
    {
        _leftLineRenderer.positionCount = 0;
        _rightLineRenderer.positionCount = 0;
        base.ExitState();
    }

    public void Zip()
    {
        _blackboard.playerController.transform.DOLookAt(_blackboard.zipPoint, 0.1f, AxisConstraint.Y);
        _blackboard.rb.DOMove(_blackboard.zipPoint, 0.6f);
    }

    public void StartZip()
    {
        _leftLineRenderer.positionCount = 2;
        _rightLineRenderer.positionCount = 2;

        SetLineRenderer(_leftLineRenderer, _leftHandTransform);
        SetLineRenderer(_rightLineRenderer, _rightHandTransform);
    }
    private void SetLineRenderer(LineRenderer lineRenderer, Transform hand)
    {
        lineRenderer.SetPosition(0, hand.position);
        lineRenderer.SetPosition(1, _blackboard.zipPoint);
    }
}
