using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Waves
{
    public List<Enemy> enemies = new List<Enemy>();

    public void SpawnWave(Vector3 position, Transform parent)
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.Spawn(position, Quaternion.identity, parent);
        }
    }

    public void DespawnWave(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
