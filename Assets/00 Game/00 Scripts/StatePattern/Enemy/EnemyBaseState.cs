using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : MonoBehaviour
{
    [SerializeField] protected float _elapsedTime;
    [SerializeField] protected EnemyStateManager _stateManager;
    [SerializeField] protected EnemyBlackboard _blackboard;

    public virtual void EnterState(EnemyStateManager stateManager, EnemyBlackboard blackboard)
    {
        _elapsedTime = 0;
        _stateManager = stateManager;
        _blackboard = blackboard;
    }

    public virtual void UpdateState()
    {
        _elapsedTime += Time.deltaTime;
    }

    public virtual void ExitState()
    {

    }
}
