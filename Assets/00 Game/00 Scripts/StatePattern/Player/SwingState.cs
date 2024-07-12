using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SwingState : AirborneMoveState
{
    [SerializeField] float swingSpeed;
    [SerializeField] float maxDistance;
    [SerializeField] float rotateSpeed;
    Vector3 swingPoint;

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
        _blackboard.character.SetRotationMode(RotationMode.None);
        _blackboard.playerController.rb.isKinematic = false;
        _blackboard.playerController.rb.velocity = velocity;
        _blackboard.playerController.rb.useGravity = true;
        _blackboard.playerController.rb.freezeRotation = true;
        Swing();
    }

    public override void UpdateState()  
    {
        base.UpdateState();
        
        Debug.DrawRay(_blackboard.playerController.transform.position, _blackboard.playerController.rb.velocity, Color.blue);

        DrawRope();

        if (!_blackboard.inputSO.buttonSwing)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.onAirState);
            return;
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        _blackboard.playerController.transform.forward = Vector3.Lerp(_blackboard.playerController.transform.forward,
            _blackboard.playerController.rb.velocity, rotateSpeed * Time.deltaTime);

        Vector2 input = _blackboard.inputSO.move;
        Vector3 horizontal = _blackboard.cam.transform.right * input.x;

        _blackboard.playerController.rb.AddForce(horizontal * swingSpeed);
    }

    public override void ExitState()
    {
        lineRenderer.positionCount = 0;

        _blackboard.playerController.transform.rotation = Quaternion.identity;
        Destroy(joint);
        Vector3 velocity = _blackboard.playerController.rb.velocity;
        _blackboard.character.SetMovementMode(MovementMode.Walking);
        _blackboard.character.SetRotationMode(RotationMode.OrientToMovement);
        _blackboard.playerController.rb.isKinematic = true;
        _blackboard.playerController.rb.useGravity = false;
        _blackboard.character.SetVelocity(velocity);
        
        base.ExitState();
    }

    void Swing()
    {
        RaycastHit hit;
        if(Physics.Raycast(grapplePoint.position, _blackboard.transform.up + _blackboard.transform.forward, out hit, maxDistance,groundLayer))
        {
            swingPoint = hit.point;
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

            lineRenderer.positionCount = 2;
        }
    }

    void DrawRope()
    {
        if(!joint) return;

        lineRenderer.SetPosition(0, grapplePoint.position);
        lineRenderer.SetPosition(1, swingPoint);
    }
}
