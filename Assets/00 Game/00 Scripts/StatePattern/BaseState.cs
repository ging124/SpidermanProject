using System;
using UnityEngine;

public enum StateStatus
{
    None,
    Success,
    Failure,
    Running
}

public abstract class BaseState : MonoBehaviour
{
    [field: SerializeField] public float _elapsedTime { get; private set; }
    [SerializeField] protected StateManager _stateManager;
    
    public virtual void InitState(StateManager stateManager, Blackboard blackboard, StateReferences stateReferences)
    {
        _stateManager = stateManager;
    }

    public virtual void EnterState() 
    {
        _elapsedTime = 0;
    }

    public virtual void ConsistentUpdateState()
    {
        _elapsedTime += Time.deltaTime;
    }

    public virtual StateStatus UpdateState()
    {
        return StateStatus.Running;
    }

    public virtual void FixedUpdateState() { }

    public virtual void ExitState() { }
}
