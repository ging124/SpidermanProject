using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Manager/EnemyManager")]
public class EnemyManager : ScriptableObject
{
    [SerializeField] private List<Enemy> enemies;

    public void Add(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void Remove(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
