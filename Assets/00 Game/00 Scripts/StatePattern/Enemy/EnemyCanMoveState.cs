using UnityEngine;

public class EnemyCanMoveState : EnemyMovementState
{
    public override void EnterState()
    {
        base.EnterState();
        _blackboard.enemyController.movement = Random.insideUnitSphere;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
