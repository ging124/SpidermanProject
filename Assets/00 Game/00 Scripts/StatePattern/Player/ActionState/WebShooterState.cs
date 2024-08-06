using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebShooterState : ActionState
{
    [SerializeField] private WebBullet _webBullet;
    [SerializeField] private ClipTransition _webShooterAnim;
    [SerializeField] private float _timeToAttack = 0.15f;

    public override void EnterState()
    {
        base.EnterState();
        RunAnimation();
    }

    public override StateStatus UpdateState()
    {
        StateStatus baseStatus = base.UpdateState();
        if (baseStatus != StateStatus.Running)
        {
            return baseStatus;
        }

        if (_blackboard.inputSO.buttonGadget && _elapsedTime > _timeToAttack)
        {
            _stateManager.ChangeState(_stateReferences.webShooterState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _actionLayer.StartFade(0, 0.1f);
        base.ExitState();
    }

    public void Shooting()
    {
        if (_blackboard.playerController.enemyTarget != null)
        {
            _webBullet.Spawn(_blackboard.playerController.leftHand.position, _blackboard.transform.rotation, null);
        }
        else
        {
            _webBullet.Spawn(_blackboard.playerController.leftHand.position, _blackboard.transform.rotation, null);
        }
    }

    public void ShootingLeftHand()
    {
        _webBullet.Spawn(_blackboard.playerController.leftHand.position, _blackboard.transform.rotation, null);
    }

    public void ShootingRightHand()
    {
        _webBullet.Spawn(_blackboard.playerController.rightHand.position, _blackboard.transform.rotation, null);
    }

    public void RunAnimation()
    {
        if(((StateManagerAction)_stateManager).stateManagerMovement.currentState != _stateReferences.idleNormalState)
        {
            _actionLayer.Play(_webShooterAnim, 0.25f, FadeMode.FromStart).Events.OnEnd = () =>
            {
                _stateManager.ChangeState(_stateReferences.idleActionState);
            };
        }
        else
        {
            _blackboard.playerController.animancer.Layers[1].Play(_webShooterAnim, 0.25f, FadeMode.FromStart).Events.OnEnd = () =>
            {
                _stateManager.ChangeState(_stateReferences.idleActionState);
            };
        }
    }
}
