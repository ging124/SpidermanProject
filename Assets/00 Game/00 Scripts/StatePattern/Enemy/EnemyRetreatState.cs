using Animancer;
using Animancer.Units;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyRetreatState : EnemyNormalState
{
    [SerializeField] private LinearMixerTransition _retreatState;
    [SerializeField] private float _changeState;
    [SerializeField] private EnemyMovementLoop enemyMovementLoop;

    public override void EnterState()
    {
        base.EnterState();
        _changeState = Random.Range(3, 8);
        enemyMovementLoop = (EnemyMovementLoop)Random.Range(0, System.Enum.GetValues(typeof(EnemyMovementLoop)).Length);
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

        if (enemyMovementLoop == EnemyMovementLoop.Forward)
        {
            if (_blackboard.enemyController.enemyData.GetType() == typeof(Boss) && _blackboard.enemyController.target != null)
            {
                float random = Random.Range(0, 1f);
                if (random < 0.2)
                {
                    int randomSkill = Random.Range(0, _stateReferences.enemySkillState.listSkill.Length);
                    if (_stateReferences.enemySkillState.listSkill[randomSkill].CanSkill(_blackboard.enemyController.target, _blackboard.enemyController.transform))
                    {
                        _stateReferences.enemySkillState.ChoseSkill(randomSkill);
                        _stateManager.ChangeState(_stateReferences.enemySkillState);
                        return StateStatus.Success;
                    }
                }
                else
                {
                    if (_blackboard.enemyController.canAttack)
                    {
                        _stateManager.ChangeState(_stateReferences.enemyAttackState);
                        return StateStatus.Success;
                    }
                }
            }
            else if (_blackboard.enemyController.enemyData.GetType() == typeof(RangeEnemy) && _blackboard.enemyController.target != null)
            {
                if (_blackboard.enemyController.canAttack)
                {
                    _stateManager.ChangeState(_stateReferences.enemyAttackState);
                    return StateStatus.Success;
                }
                else
                {
                    _stateManager.ChangeState(_stateReferences.enemyAimState);
                    return StateStatus.Success;
                }
            }
            else
            {
                if (_blackboard.enemyController.canAttack)
                {
                    _stateManager.ChangeState(_stateReferences.enemyAttackState);
                    return StateStatus.Success;
                }
            }
        }

        switch (_blackboard.enemyController.hitAttackType)
        {
            case AttackType.NormalAttack:
                _stateManager.ChangeState(_stateReferences.enemyHitState);
                return StateStatus.Success;
            case AttackType.HeavyAttack:
                _stateManager.ChangeState(_stateReferences.enemyKnockDownState);
                return StateStatus.Success;
        }

        if (_elapsedTime >= _changeState)
        {
            _stateManager.ChangeState(_stateReferences.enemyRetreatState);
            return StateStatus.Success;
        }

        return StateStatus.Running;
    }

    public override void FixedUpdateState()
    {
        if (_blackboard.enemyController.target != null)
        {
            Vector3 target = _blackboard.enemyController.target.transform.position;
            target.y = _blackboard.enemyController.transform.position.y;
            Vector3 lookAt = target - _blackboard.enemyController.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(lookAt.normalized, Vector3.up);
            _blackboard.enemyController.transform.rotation = Quaternion.Lerp(_blackboard.enemyController.transform.rotation, targetRotation, Time.fixedDeltaTime * _blackboard.enemyController.enemyData.rotationSpeed);

            Vector3 direction = Vector3.zero;
            float speed = _blackboard.enemyController.enemyData.moveSpeed;

            switch (enemyMovementLoop)
            {
                case EnemyMovementLoop.Left:
                    direction = Quaternion.AngleAxis(90, Vector3.up) * lookAt;
                    break;
                case EnemyMovementLoop.Right:
                    direction = Quaternion.AngleAxis(-90, Vector3.up) * lookAt;
                    break;
                case EnemyMovementLoop.Forward:
                    direction = lookAt;
                    speed *= 5;
                    break;
                case EnemyMovementLoop.Back:
                    direction = -lookAt;
                    break;
            }

            _blackboard.enemyController.characterController.Move(direction.normalized * Time.fixedDeltaTime * speed);

            _retreatState.State.Parameter = Vector3.SignedAngle(lookAt.normalized, direction.normalized, Vector3.up);

        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public enum EnemyMovementLoop
    {
        Left,
        Right,
        Back,
        Forward
    }

}
