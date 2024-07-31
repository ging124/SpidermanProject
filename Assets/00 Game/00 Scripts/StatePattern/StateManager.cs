using NodeCanvas.BehaviourTrees;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private bool _isAIControlled = false;
    [SerializeField] private BaseState startState;
    [field: SerializeField] public BaseState currentState { get; private set; }
    [SerializeField] private StateReferences stateReferences;
    [SerializeField] private Blackboard blackboard;

    public void Init(bool isAIController)
    {
        blackboard = transform.parent.GetComponent<Blackboard>();
        _isAIControlled = transform.parent.GetComponent<BehaviourTreeOwner>() != null;
        foreach (Transform stateTransform in this.transform)
        {
            var state = stateTransform.GetComponent<BaseState>();
            state.InitState(this, blackboard, stateReferences);
        }
        currentState = startState;
        currentState.EnterState();
    }

    public virtual void Start()
    {
        Init(_isAIControlled);
    }

    public virtual void ChangeState(BaseState state)
    {
        if (_isAIControlled)
        {
            return;
        }

        currentState.ExitState();
        currentState = state;
        currentState.EnterState();
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
