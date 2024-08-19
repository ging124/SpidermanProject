using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyController : EnemyController
{
    public EnemyBullet enemyBullet;
    public Transform gunTransform;
    public LineRenderer lineRenderer;

    protected override void OnEnable()
    {
        currentHP = ((RangeEnemy)enemyData).maxHP;
        base.OnEnable();
    }
}
