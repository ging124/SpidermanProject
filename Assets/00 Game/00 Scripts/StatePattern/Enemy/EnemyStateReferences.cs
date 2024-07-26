using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateReferences : MonoBehaviour
{
    public EnemyIdleState enemyIdleState;
    public EnemyIdleCombatState enemyIdleCombatState;
    public EnemyMoveState enemyMoveState;
    public EnemyRunState enemyRunState;
    public EnemyHitState enemyHitState;
    public EnemyDeadState enemyDeadState;

    public EnemyIdleActionState enemyIdleActionState;
    public EnemyAttack1State enemyAttack1State;
    public EnemyAttack2State enemyAttack2State;
}
