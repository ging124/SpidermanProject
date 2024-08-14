using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemyController : EnemyController
{
    protected override void OnEnable()
    {
        currentHP = ((MeleEnemy)enemyData).maxHP;
        base.OnEnable();
    }
}
