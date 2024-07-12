using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NormalState : BaseState
{
    [SerializeField] protected AnimancerLayer _normalBodyLayer;

    public Transform orientation;
    public LayerMask wallLayer;

    public float detectionLength;
    public float sphereCastRadius;
    public float wallLookAngle;

    private bool wallFront;
    private RaycastHit frontWallHit;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer = _blackboard.animancer.Layers[0];
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(_blackboard.playerController.transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, wallLayer);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);
    }
}
