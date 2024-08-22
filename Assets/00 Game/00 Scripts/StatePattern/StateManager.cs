using NodeCanvas.BehaviourTrees;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private bool _isAIControlled = false;
    [SerializeField] private BaseState _startState;
    [field: SerializeField] public BaseState currentState { get; private set; }
    [SerializeField] private StateReferences _stateReferences;
    [SerializeField] private Blackboard _blackboard;

    public void Init(bool isAIController)
    {
        _blackboard = transform.parent.GetComponent<Blackboard>();
        _isAIControlled = transform.parent.GetComponent<BehaviourTreeOwner>() != null;
        foreach (Transform stateTransform in this.transform)
        {
            var state = stateTransform.GetComponent<BaseState>();
            state.InitState(this, _blackboard, _stateReferences);
        }
        currentState = _startState;
        currentState.EnterState();
    }

    public virtual void OnEnable()
    {
        Init(_isAIControlled);
    }

    public bool ChangeState(BaseState state, bool force = false)
    {
        if (_isAIControlled && !force)
        {
            return false;
        }

        currentState.ExitState();
        currentState = state;
        currentState.EnterState();

        return true;
    }

    public virtual void Update()
    {
        if (!_isAIControlled)
            OnUpdate();
    }

    public StateStatus OnUpdate()
    {
        currentState.ConsistentUpdateState();
        return currentState.UpdateState();
    }

    private void FixedUpdate()
    {
        if (!_isAIControlled)
            OnFixedUpdate();
    }

    public void OnFixedUpdate()
    {
        currentState.FixedUpdateState();
    }
}
