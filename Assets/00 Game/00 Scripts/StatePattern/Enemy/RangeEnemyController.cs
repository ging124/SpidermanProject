using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyController : EnemyController
{
    protected override void OnEnable()
    {
        
        currentHP = ((RangeEnemy)enemyData).maxHP;
        base.OnEnable();
    }
}
