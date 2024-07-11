using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public BaseState startState;
    public BaseState currentState;
    public StateReferences stateReferences;
    public Blackboard blackboard;

    public virtual void Start()
    {
        currentState = startState;
        currentState?.EnterState(this, blackboard);
    }

    public virtual void ChangeState(BaseState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState(this, blackboard);
    }

    public virtual void Update()
    {
        currentState.UpdateState();
    }

    public virtual void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }
}
