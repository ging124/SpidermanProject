using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RPGObject/Enemy")]
public class Enemy : RPGObject, IFactory
{
    public float dectectionRange;
    public float rotateSpeed;
    public float canAttackMaxDistance;
    public float attackRange;
    public LayerMask playerLayer;

    public GameObject damageText;
    public ParticleSystem enemyDeadEffect;
    public GameObject enemyPrefab;
    Queue<GameObject> enemyPool = new Queue<GameObject>();

    public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (enemyPool.Count > 0)
        {
            var enemy = enemyPool.Dequeue().GetComponent<EnemyController>();
            enemy.transform.position = position;
            enemy.transform.rotation = rotation;
            enemy.transform.SetParent(parent);
            enemy.gameObject.SetActive(true);
            return enemy.gameObject;
        }
        else
        {
            var enemy = Instantiate(enemyPrefab, position, rotation, parent);
            enemy.GetComponent<EnemyController>().enemyData = this;
            return enemy;
        }
    }
    
    public void Despawn(GameObject prefab)
    {
        prefab.SetActive(false);
        enemyPool.Enqueue(prefab);
        /*ParticleSystem deadEffect = Instantiate(enemyDeadEffect, prefab.transform.position, Quaternion.identity);
        deadEffect.Play();*/
    }
}
