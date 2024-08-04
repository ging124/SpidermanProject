using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateReferences : StateReferences
{
    public EnemyIdleState enemyIdleState;
    public EnemyMoveState enemyMoveState;
    public EnemyRunState enemyRunState;
    public EnemyHitState enemyHitState;
    public EnemyDeadState enemyDeadState;

    public EnemyAttack1State enemyAttack1State;
    public EnemyAttack2State enemyAttack2State;
}
