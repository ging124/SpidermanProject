using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateReferences : MonoBehaviour
{
    [Header("MovementState")]
    public IdleNormalState idleNormalState;
    public MoveState moveState;
    public RunState runState;
    public StopMoveState stopMoveState;
    public StopRunState stopRunState;
    public StartJumpState startJumpState;
    public StartJumpHighState startJumpHighState;
    public OnAirState onAirState;
    public EndJumpState endJumpState;
    public EndJumpToWalkState endJumpToWalkState;
    public EndJumpToRunState endJumpToRunState;
    public HitState hitState;
    public DeadState deadState;
    public SwingState swingState;
    public DiveState diveState;
    public SwingJumpState swingJumpState;
    public ClimbState climbState;
    public ClimbMovementState climbMovementState;
    public ExitClimbState exitClimbState;


    [Header("ActionState")]
    public IdleActionState idleActionState;
    public Attack1State attack1State;
    public Attack2State attack2State;
    public Attack3State attack3State;
}
