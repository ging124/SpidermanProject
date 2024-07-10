using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateReferences : MonoBehaviour
{
    [Header("MovementState")]
    public IdleNormalState idleState;
    public StartMoveState startMoveState;
    public MoveState moveState;
    public RunState runState;
    public StopMoveState stopMoveState;
    public StartJumpState startJumpState;
    public OnAirState onAirState;
    public EndJumpState endJumpState;
    public HitState hitState;
    public DeadState deadState;
    public SwingState swingState;

    [Header("ActionState")]
    public IdleActionState idleActionState;
    public Attack1State attack1State;
    public Attack2State attack2State;
    public Attack3State attack3State;
}
