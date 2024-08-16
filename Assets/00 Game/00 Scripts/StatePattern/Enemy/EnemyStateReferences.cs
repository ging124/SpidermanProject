using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateReferences : StateReferences
{
    public EnemyIdleState enemyIdleState;
    public EnemyMoveState enemyMoveState;
    public EnemyRunState enemyRunState;
    public EnemyHitState enemyHitState;
    public EnemyKnockDownState enemyKnockDownState;
    public EnemyStandUpState enemyStandUpState;
    public EnemyDeadState enemyDeadState;
    public EnemyStunLockState enemyStunLockState;

    public EnemyMeleAttackState enemyAttackState;
    public EnemySkillState enemySkillState;
}
