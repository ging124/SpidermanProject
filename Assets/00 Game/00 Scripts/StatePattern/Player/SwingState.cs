using Animancer;
using EasyCharacterMovement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SwingState : AirborneMoveState
{
    [SerializeField] ClipTransition swingState;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform rayCast;
    [SerializeField] Vector3 grapplePoint;
    [SerializeField] float maxDistance;
    [SerializeField] float tValue;
    [SerializeField] float swingValue;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        //_normalBodyLayer.Play(swingState);
        lineRenderer.enabled = true;
        Swing();
    }

    public override void UpdateState()  
    {
        base.UpdateState();
        
        if (grapplePoint != null)
        {
            Vector3 Gpara = Vector3.Project(_blackboard.character.GetGravityVector(), (grapplePoint - _blackboard.playerController.transform.position));
            Vector3 Gperp = (_blackboard.character.GetGravityVector() - Gpara).normalized;
            Vector3 tDirection = ((Mathf.Pow(_blackboard.character.GetVelocity().magnitude, 2) / (grapplePoint - _blackboard.playerController.transform.position).magnitude)
                * (grapplePoint - _blackboard.playerController.transform.position)).normalized;
            Debug.Log(Gpara.magnitude);
            Debug.DrawRay(_blackboard.playerController.transform.position, tDirection, Color.red);
            Debug.DrawRay(_blackboard.playerController.transform.position, Gpara, Color.yellow);
            Debug.DrawRay(_blackboard.playerController.transform.position, Gperp, Color.blue);

            _blackboard.character.AddForce(Gperp * swingValue + tDirection * tValue);
            lineRenderer.SetPosition(0, _blackboard.playerController.transform.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }

        if (!_blackboard.inputSO.buttonSwing)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.onAirState);
        }
    }

    public override void ExitState()
    {
        lineRenderer.enabled = false;
        /*Vector3 velocity = _blackboard.rb.velocity.projectedOnPlane(Vector3.up);
        _blackboard.character.SetMovementMode(MovementMode.Walking);
        _blackboard.rb.useGravity = false;
        _blackboard.rb.isKinematic = true;
        _blackboard.rb.constraints = RigidbodyConstraints.None;
        _blackboard.character.SetVelocity(velocity);*/
        base.ExitState();
    }

    void Swing()
    {
        grapplePoint = rayCast.transform.position;
    }
}
