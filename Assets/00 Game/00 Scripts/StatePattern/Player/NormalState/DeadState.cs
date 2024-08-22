using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : NormalState
{
    [SerializeField] private ClipTransition _deadAnim;

    public override void EnterState()
    {
        base.EnterState();
        _blackboard.playerController.canAttack = false;
        _blackboard.playerController.canHit = false;
        _normalBodyLayer.Play(_deadAnim);
        StartCoroutine(Dead());
    }
    public override StateStatus UpdateState()
    {
        if(_blackboard.playerController.currentHP == _blackboard.playerController.playerData.maxHP)
        {
            _stateManager.ChangeState(_stateReferences.idleNormalState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _blackboard.playerController.canAttack = true;
        _blackboard.playerController.canHit = true;
        base.ExitState();
    }

    public IEnumerator Dead()
    {
        yield return new WaitForSeconds(1.6f);
        _blackboard.playerController.playerData.Despawn(_blackboard.playerController.gameObject);
        _blackboard.playerController.playerDead.Raise();
    }
}
