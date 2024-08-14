using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    protected override void OnEnable()
    {
        currentHP = ((Boss)enemyData).maxHP;
        base.OnEnable();
    }
}
