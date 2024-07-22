using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

public class SwingState : AirborneMoveState
{
    [SerializeField] float swingSpeed;
    [SerializeField] float maxDistance;
    [SerializeField] float rotateSpeed;
    [SerializeField] Transform point;
    private Vector3 swingPoint;

    [Header("References")]
    [SerializeField] SpringJoint joint;
    [SerializeField] Transform grapplePoint;
    [SerializeField] ClipTransition swingAnim;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] LayerMask groundLayer;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);

        _normalBodyLayer.Play(swingAnim);

        Vector3 velocity = _blackboard.character.GetVelocity();

        _blackboard.character.SetMovementMode(MovementMode.None);
        _blackboard.playerController.rb.isKinematic = false;
        _blackboard.playerController.rb.useGravity = true;
        _blackboard.rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        swingPoint = point.transform.position + new Vector3(0, Random.Range(0, 1), Random.Range(0, 10));

        if(velocity.magnitude < 20)
        {
            _blackboard.rb.velocity = velocity * 1.5f;
        }
        else if (velocity.magnitude > 80)
        {
            _blackboard.rb.velocity = velocity * 0.9f;
        }
        else
        {
            _blackboard.rb.velocity = velocity;
        }

        Swing();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        DrawRope();

        if (!_blackboard.inputSO.buttonSwing)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.swingJumpState);
            return;
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        Quaternion rotation = Quaternion.LookRotation(_blackboard.playerController.transform.forward, (swingPoint - _blackboard.playerController.transform.position).normalized);
        _blackboard.playerController.transform.rotation = Quaternion.Lerp(_blackboard.playerController.transform.rotation, rotation, 0.2f * Time.deltaTime);
        _blackboard.character.RotateTowardsWithSlerp(_blackboard.playerController.rb.velocity.normalized, false);
        
        Vector2 input = _blackboard.inputSO.move;
        Vector3 horizontal = _blackboard.cam.transform.right * input.x;

        _blackboard.playerController.rb.AddForce(horizontal.normalized * swingSpeed * Time.fixedDeltaTime * 50);
    }

    public override void ExitState()
    {
        lineRenderer.positionCount = 0;

        Destroy(joint);
        _blackboard.playerController.transform.DORotate(Quaternion.LookRotation(_blackboard.playerController.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles, 0.2f);
        Vector3 velocity = _blackboard.rb.velocity;
        _blackboard.character.SetMovementMode(MovementMode.Falling);
        _blackboard.playerController.rb.useGravity = false;
        _blackboard.playerController.rb.isKinematic = true;
        _blackboard.rb.constraints = RigidbodyConstraints.None;
        _blackboard.character.SetVelocity(velocity);

        base.ExitState();
    }

    void Swing()
    {
        joint = _blackboard.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = new Vector3(0, 1.4f, 0);
        joint.connectedAnchor = swingPoint;

        float distanceFromPoint = Vector3.Distance(_blackboard.playerController.transform.position, swingPoint);

        joint.maxDistance = distanceFromPoint * 0.5f;
        joint.minDistance = distanceFromPoint * 0.4f;

        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;

        lineRenderer.positionCount = 3;
    }

    void DrawRope()
    {
        if (!joint) return;

        lineRenderer.SetPosition(0, grapplePoint.position + Vector3.down / 3f);
        lineRenderer.SetPosition(1, grapplePoint.position);
        lineRenderer.SetPosition(2, swingPoint);
    }
}
