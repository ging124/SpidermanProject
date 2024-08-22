using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GroupBehaviour/EnemyGroupBehaviour")]
public class EnemyGroupBehaviour : ScriptableObject
{
    [SerializeField] public List<EnemyController> listEnemy;

    private void OnValidate()
    {
        listEnemy.Clear();
    }

    public void RegisterEnemy(EnemyController enemy)
    {
        listEnemy.Add(enemy);
    }

    public void UnregisterEnemy(EnemyController enemy)
    {
        listEnemy.Remove(enemy);
    }

    public void DestroyAllEnemy()
    {
        for(int i = listEnemy.Count - 1; i >= 0; i--)
        {
            listEnemy[i].enemyData.Despawn(listEnemy[i].gameObject);
        }
    }
}
