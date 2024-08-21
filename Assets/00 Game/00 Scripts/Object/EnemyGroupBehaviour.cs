using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GroupBehaviour/EnemyGroupBehaviour")]
public class EnemyGroupBehaviour : ScriptableObject
{
    [SerializeField] private List<Enemy> listEnemy;

    private void OnValidate()
    {
        listEnemy.Clear();
    }

    public void RegisterEnemy(Enemy enemy)
    {
        listEnemy.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        listEnemy.Remove(enemy);
    }
}
