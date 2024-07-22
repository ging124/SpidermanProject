using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

public class StartZipState : NormalState
{
    [SerializeField] private ClipTransition _zipAnim;
    [SerializeField] private float _timeToChangeState;
    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;
    [SerializeField] private Transform _leftHandTransform;
    [SerializeField] private Transform _rightHandTransform;
    [SerializeField] private Vector3 zipPoint;


    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_zipAnim);
        _blackboard.character.SetMovementMode(MovementMode.None);
        if (_blackboard.zipLength / 100f >= 0.5f)
        {
            _timeToChangeState = _blackboard.zipLength / 100f;
        }
        else
        {
            _timeToChangeState = 0.5f;
        }

        zipPoint = _blackboard.zipPoint;
        _blackboard.zipIconImage.gameObject.SetActive(true);
        Camera camera = _blackboard.cam.GetComponent<Camera>();
        _blackboard.zipIconImage.transform.position = camera.WorldToScreenPoint(_blackboard.zipPoint);
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
        _blackboard.character.SetMovementMode(MovementMode.Walking);
        base.ExitState();
    }

    public void Zip()
    {
        _blackboard.playerController.transform.DOLookAt(zipPoint, 0.1f, AxisConstraint.Y);
        _blackboard.rb.DOMove(zipPoint + Vector3.up, _timeToChangeState);
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
        lineRenderer.SetPosition(1, zipPoint);
    }
}
