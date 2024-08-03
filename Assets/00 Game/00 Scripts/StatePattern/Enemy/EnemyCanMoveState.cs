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

    protected virtual void Movement(float speed)
    {
        _blackboard.enemyController.transform.position += new Vector3(_blackboard.enemyController.movement.x, 0, _blackboard.enemyController.movement.z) * speed * Time.deltaTime;

        if (_blackboard.enemyController.movement != Vector3.zero)
        {
            _blackboard.enemyController.transform.forward = Vector3.Lerp(_blackboard.transform.forward, new Vector3(_blackboard.enemyController.movement.x, 0, _blackboard.enemyController.movement.z), _blackboard.enemyController.enemyData.rotateSpeed * Time.deltaTime);
        }
    }

    protected virtual void Movement(Vector3 direction)
    {
        _blackboard.enemyController.agent.SetDestination(direction);

        if (_blackboard.enemyController.movement != Vector3.zero)
        {
            _blackboard.enemyController.transform.forward = Vector3.Lerp(_blackboard.enemyController.transform.forward, new Vector3(_blackboard.enemyController.movement.x, 0, _blackboard.enemyController.movement.z), _blackboard.enemyController.enemyData.rotateSpeed * Time.deltaTime);
        }
    }
}
