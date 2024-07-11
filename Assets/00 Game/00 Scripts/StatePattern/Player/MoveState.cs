using Animancer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MoveState : MovementState
{
    [SerializeField] private LinearMixerTransition _moveBlendTree;

    public override void EnterState(StateManager stateManager, Blackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);
        _normalBodyLayer.Play(_moveBlendTree);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Movement();

        _moveBlendTree.State.Parameter = Mathf.Lerp(_moveBlendTree.State.Parameter, _blackboard.character.GetSpeed(), 55 * Time.deltaTime);

        if (_stateManager.currentState != this)
        {
            return;
        }

        if (_blackboard.inputSO.move == Vector2.zero)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.stopMoveState);
            return;
        }

        if (_blackboard.inputSO.buttonRun)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.runState);
            return;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
