using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

public class SwingState : OnAirState
{
    [SerializeField] float rotateSpeed;
    private Vector3 swingPoint;

    [Header("References")]
    [SerializeField] SpringJoint joint;
    [SerializeField] ClipTransition swingAnim;
    [SerializeField] LineRenderer lineRenderer;

    public override void EnterState()
    {
        base.EnterState();

        _normalBodyLayer.Play(swingAnim);

        Vector3 velocity = _blackboard.character.GetVelocity();

        _blackboard.character.SetMovementMode(MovementMode.None);
        _blackboard.playerController.rb.useGravity = true;
        _blackboard.playerController.rb.isKinematic = false;
        _blackboard.playerController.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        Transform cam = _blackboard.playerController.cam;
        swingPoint = _blackboard.playerController.transform.position + Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized * 30 + Vector3.up * 25;

        if(velocity.magnitude < 10)
        {
            _blackboard.playerController.rb.AddForce(_blackboard.playerController.transform.forward * 1500f);
        }
        else
        {
            _blackboard.playerController.rb.velocity = velocity;
        }

        _blackboard.playerController.detectionLength = 3;

        Swing();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        DrawRope();

        if (!_blackboard.inputSO.buttonJump && _elapsedTime > 0.2f/* || _blackboard.playerController.transform.position == Vector3.Reflect()*/)
        {
            _stateManager.ChangeState(_stateReferences.swingJumpState);
            return StateStatus.Success;
        }

        if (_blackboard.playerController.groundHit)
        {
            float distanceFromPoint = Vector3.Distance(_blackboard.transform.position, swingPoint);

            joint.spring = 6;
            joint.maxDistance = distanceFromPoint * 0.1f;
            joint.minDistance = distanceFromPoint * 0.1f;
        }

        return StateStatus.Running;
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        Rotation();

        /*Vector2 input = _blackboard.inputSO.move;
        Vector3 horizontal = _blackboard.cam.transform.right * input.x;

        _blackboard._playerController.rb.AddForce(horizontal.normalized * swingSpeed * Time.fixedDeltaTime * 50);*/
    }

    public override void ExitState()
    {
        lineRenderer.positionCount = 0;
        Destroy(joint);
        _blackboard.playerController.detectionLength = 1;

        Vector3 velocity = _blackboard.playerController.rb.velocity;
        _blackboard.character.SetMovementMode(MovementMode.Falling);
        _blackboard.playerController.rb.useGravity = false;
        _blackboard.playerController.rb.isKinematic = true;
        _blackboard.playerController.rb.constraints = RigidbodyConstraints.None;
        _blackboard.character.SetVelocity(velocity);

        base.ExitState();
    }

    void Swing()
    {
        joint = _blackboard.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = new Vector3(0, 1.4f, 0);
        joint.connectedAnchor = swingPoint;

        float distanceFromPoint = Vector3.Distance(_blackboard.transform.position, swingPoint);

        joint.maxDistance = distanceFromPoint * 0.6f;
        joint.minDistance = distanceFromPoint * 0.3f;

        
        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;

        lineRenderer.positionCount = 2;
    }

    void Rotation()
    {
        Quaternion rotation = Quaternion.LookRotation(_blackboard.playerController.transform.forward, (swingPoint - _blackboard.playerController.transform.position).normalized);
        _blackboard.transform.rotation = Quaternion.Lerp(_blackboard.playerController.transform.rotation, rotation, 0.2f * rotateSpeed * Time.fixedDeltaTime);
        _blackboard.character.RotateTowardsWithSlerp(_blackboard.playerController.rb.velocity.normalized, false);
    }

    void DrawRope()
    {
        if (!joint) return;

        lineRenderer.SetPosition(0, _blackboard.playerModel.rightHand.position);
        lineRenderer.SetPosition(1, swingPoint);
    }
}
