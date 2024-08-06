using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateReferences : StateReferences
{
    [Header("MovementState")]
    public IdleNormalState idleNormalState;
    public RunState runState;
    public StopRunState stopRunState;
    public LandState landState;
    public LandLowState landLowState;
    public LandHighState landHighState;
    public StartJumpState jumpState;
    public FallState fallState;
    public HitState hitState;
    public DeadState deadState;
    public SwingState swingState;
    public DiveState diveState;
    public SwingJumpState swingJumpState;
    public ClimbIdleState climbState;
    public ClimbJumpState climbJumpState;
    public ClimbMovementState climbMovementState;
    public ExitClimbState exitClimbState;
    public StartZipState startZipState;
    public IdleZipState idleZipState;
    public ZipJumpState zipJumpState;
    public SwimState swimState;
    public DodgeState dodgeState;

    [Header("ActionState")]
    public NormalActionState normalActionState;
    public IdleActionState idleActionState;
    public MeleAttackState meleAttackState;
    public NearAttackState nearAttackState;
    public MediumAttackState mediumAttackState;
    public FarAttackState farAttackState;
    public WebShooterState webShooterState;
    public UltimateAttackState ultimateAttackState;
}
