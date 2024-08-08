using Animancer;
using UnityEngine;

public class UseGadgetState : ActionState
{
    [SerializeField] private WebBullet _webBullet;
    [SerializeField] private float _timeToAttack = 0.15f;

    public override void EnterState()
    {
        base.EnterState();
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
            _stateManager.ChangeState(_stateReferences.useGadgetState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void ExitState()
    {
        _actionLayer.StartFade(0, 0.1f);
        base.ExitState();
    }
}
