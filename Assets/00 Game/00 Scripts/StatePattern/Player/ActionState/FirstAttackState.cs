using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAttackState : AttackState
{
    private FirstHit firstHit;

    public override void EnterState(StateManager stateManager, PlayerBlackboard blackboard)
    {
        base.EnterState(stateManager, blackboard);

        //_actionLayer.Play(_listCombo[combo].hitList[hit].hitAnim);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        /*if (!_blackboard.inputSO.buttonAttack && _elapsedTime > _listCombo[combo].hitList[hit].timeEndAttack)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.idleActionState);
        }

        if (_blackboard.inputSO.buttonAttack && _elapsedTime > _listCombo[combo].hitList[hit].timeNextAttack)
        {
            _stateManager.ChangeState(_stateManager.stateReferences.meleAttackState);
        }*/
    }

    public override void ExitState()
    {
        base.ExitState();
    }


}
