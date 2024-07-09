using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RollState : NormalState
{
    [SerializeField] private float _timeChangeState = 0.5f;
    [SerializeField] private float _timeToRoll = 0.28f;
    [SerializeField] private ClipTransition _normalRoll;
    [SerializeField] private ClipTransition _backRoll;
    [SerializeField] private bool backRoll;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);

        if (_blackboard.inputSO.move != Vector2.zero)
        {
            _blackboard.animancer.Play(_normalRoll);
            backRoll = false;
        }
        else
        {
            _blackboard.animancer.Play(_backRoll);
            backRoll = true;
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_elapsedTime > _timeToRoll && !backRoll)
        {
            Roll(_blackboard.transform.forward);    
        }
        else if(_elapsedTime > _timeToRoll && backRoll)
        {
            Roll(-_blackboard.transform.forward);
        }
        
        if (!_blackboard.inputSO.buttonRoll && _elapsedTime > _timeChangeState)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        backRoll = false;
    }

    public virtual void Roll(Vector3 velocity)
    {
        _blackboard.charController.Move(velocity * _blackboard.playerData.rollVelocity * Time.deltaTime);
    }
}
