using Animancer;
using UnityEngine;

public class IdleActionState : ActionState
{
    [SerializeField] private float _timeToAttack;
    [SerializeField] private float _timeToChangeState;
    [SerializeField] private ClipTransition _idleActionAnim;


    public override void EnterState()
    {
        base.EnterState();
        _actionLayer.Play(_idleActionAnim);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_elapsedTime > _timeToChangeState || ((StateManagerAction)_stateManager).stateManagerMovement.currentState != _stateReferences.idleNormalState)
        {
            _stateManager.ChangeState(_stateReferences.normalActionState);
            return StateStatus.Success;
        }

        if (_blackboard.inputSO.buttonAttack && _elapsedTime > _timeToAttack
            && ((StateManagerAction)_stateManager).stateManagerMovement != _stateReferences.deadState)
        {
            if (_blackboard.playerController.enemyTarget == null)
            {
                _stateManager.ChangeState(_stateReferences.meleAttackState);
                return StateStatus.Success;
            }
            else
            {
                Vector3 player = _blackboard.playerController.transform.position;
                Vector3 target = _blackboard.playerController.enemyTarget.transform.position;
                float distance = Vector3.Distance(player, target);
                if (distance >= _blackboard.playerController.farAttackRange)
                {
                    _stateManager.ChangeState(_stateReferences.farAttackState);
                    return StateStatus.Success;
                }
                else if (distance >= _blackboard.playerController.mediumAttackRange && distance < _blackboard.playerController.farAttackRange)
                {
                    _stateManager.ChangeState(_stateReferences.mediumAttackState);
                    return StateStatus.Success;
                }
                else if (distance >= _blackboard.playerController.nearAttackRange && distance < _blackboard.playerController.mediumAttackRange)
                {
                    _stateManager.ChangeState(_stateReferences.nearAttackState);
                    return StateStatus.Success;
                }
                else
                {
                    _stateManager.ChangeState(_stateReferences.meleAttackState);
                    return StateStatus.Success;
                }
            }
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _actionLayer.StartFade(0, 0.1f);
        base.ExitState();
    }
}
