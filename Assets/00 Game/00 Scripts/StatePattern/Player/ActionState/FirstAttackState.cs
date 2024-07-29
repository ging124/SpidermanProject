using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FirstAttackState : AttackState
{
    [SerializeField] private FirstHit firstHit;
    [SerializeField] private LineRenderer _rightLineRenderer;


    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);

        if (_blackboard.playerController.enemyTarget != null)
        {
            DirectionAttack();
        }
        else
        {   
            _actionLayer.Play(firstHit.nearAttack.hitAnim);
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!_blackboard.inputSO.buttonAttack && _elapsedTime > 0.4f)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleActionState);
        }

        if (_blackboard.inputSO.buttonAttack && _elapsedTime > 0.2f)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.meleAttackState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private void DirectionAttack()
    {
        float range = Vector3.Distance(_blackboard.playerController.enemyTarget.transform.position, _blackboard.playerController.transform.position);
        if(range >= _blackboard.playerController.mediumAttackRange && range < _blackboard.playerController.farAttackRange)
        {
            _actionLayer.Play(firstHit.mediumAttack.hitAnim);
            firstHit.mediumAttack.hitAnim.Events.SetCallback("MoveToTarget", MoveToTarget);
            firstHit.mediumAttack.hitAnim.Events.SetCallback("Attack", Attack);

        }
        else if(range >= _blackboard.playerController.farAttackRange)
        {
            _actionLayer.Play(((FarHit)firstHit.farAttack).zipAnim);
            ((FarHit)firstHit.farAttack).zipAnim.Events.SetCallback("Zip", Zip);
            ((FarHit)firstHit.farAttack).zipAnim.Events.SetCallback("MoveToTarget", MoveToTarget);
            ((FarHit)firstHit.farAttack).zipAnim.Events.SetCallback("Attack", Attack);

        }
        else if (range < _blackboard.playerController.mediumAttackRange)
        {
            _actionLayer.Play(firstHit.nearAttack.hitAnim);
            firstHit.nearAttack.hitAnim.Events.SetCallback("MoveToTarget", MoveToTarget);
            firstHit.nearAttack.hitAnim.Events.SetCallback("Attack", Attack);

        }
    }

    public void MoveToTarget()
    {
        _rightLineRenderer.positionCount = 0;

        Vector3 distance = _blackboard.playerController.enemyTarget.transform.position - _blackboard.playerController.transform.position;
        Vector3 endValue = _blackboard.playerController.enemyTarget.transform.position - distance * 0.1f;
        endValue.y = _blackboard.playerController.transform.position.y;

        if(distance.magnitude < 1.5f) return;

        _blackboard.playerController.transform.DOMove(endValue, 0.2f);
    }

    public void Zip()
    {
        _rightLineRenderer.positionCount = 2;
        _blackboard.playerController.SetLineRenderer(_rightLineRenderer, _blackboard.playerController.rightHand, _blackboard.playerController.enemyTarget.transform.position);
    }
}
