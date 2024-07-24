using System;
using UnityEngine;

[Serializable]
public abstract class BaseState : MonoBehaviour
{
    public float _elapsedTime;
    [SerializeField] protected StateManager _stateManager;
    [SerializeField] protected Blackboard _blackboard;

    public virtual void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        _elapsedTime = 0;
        _stateManager = stateManager;
        _blackboard = blackboard;
    }

    public virtual void UpdateState()
    {
        _elapsedTime += Time.deltaTime;
    }

    public virtual void FixedUpdateState() { }

    public virtual void ExitState() { }
}
