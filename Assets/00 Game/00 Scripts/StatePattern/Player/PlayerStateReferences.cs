using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateReferences : StateReferences
{
    [Header("MovementState")]
    public IdleNormalState idleNormalState;
    public MoveState moveState;
    public RunState runState;
    public StopMoveState stopMoveState;
    public StopRunState stopRunState;
    public StartJumpState jumpState;
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
    public ClimbJumpState climbJumpState;
    public ClimbMovementState climbMovementState;
    public ExitClimbState exitClimbState;
    public StartZipState startZipState;
    public IdleZipState idleZipState;
    public ZipJumpState zipJumpState;
    public SwimState swimState;


    [Header("ActionState")]
    public NormalActionState normalActionState;
    public IdleActionState idleActionState;
    public MeleAttackState meleAttackState;
    public FirstAttackState firstAttackState;
}
