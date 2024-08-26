using Animancer;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class EnemyRetreatState : EnemyNormalState
{
    [SerializeField] private LinearMixerTransition _retreatState;


    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("EnterRetreatState");
        _normalBodyLayer.Play(_retreatState);
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.enemyController.target == null)
        {
            _stateManager.ChangeState(_stateReferences.enemyIdleState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void FixedUpdateState()
    {
        if(_blackboard.enemyController.target != null)
        {
            Vector3 target = _blackboard.enemyController.target.transform.position;
            target.y = _blackboard.enemyController.transform.position.y;
            Vector3 lookAt = target - _blackboard.enemyController.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(lookAt.normalized, Vector3.up);

            _blackboard.enemyController.rigid.MovePosition(_blackboard.enemyController.transform.position + target * Time.fixedDeltaTime * _blackboard.enemyController.enemyData.moveSpeed);
            _blackboard.enemyController.rigid.Move(_blackboard.enemyController.transform.position + _blackboard.enemyController.transform.right * Time.fixedDeltaTime * _blackboard.enemyController.enemyData.moveSpeed
                , Quaternion.Lerp(_blackboard.enemyController.transform.rotation, targetRotation, Time.fixedDeltaTime * _blackboard.enemyController.enemyData.rotationSpeed));

            _retreatState.State.Parameter = Mathf.Lerp(_retreatState.State.Parameter, _blackboard.enemyController.rigid.velocity.x, 60 * Time.fixedDeltaTime);
            Debug.Log(_blackboard.enemyController.rigid.velocity);

        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
