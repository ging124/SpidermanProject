using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RPGObject/NPCProtect")]
public class NPCProtect : RPGObject
{
    public GameObject npcProtectPrefab;
    Queue<GameObject> npcProtectPool = new Queue<GameObject>();

    public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (npcProtectPool.Count > 0)
        {
            var enemy = npcProtectPool.Dequeue().GetComponent<NPCProtectController>();
            enemy.transform.position = position;
            enemy.transform.rotation = rotation;
            enemy.transform.SetParent(parent);
            enemy.gameObject.SetActive(true);
            return enemy.gameObject;
        }
        else
        {
            var enemy = Instantiate(npcProtectPrefab, position, rotation, parent);
            enemy.GetComponent<NPCProtectController>().npcProtecData = this;
            enemy.gameObject.SetActive(true);
            return enemy;
        }
    }

    public void Despawn(GameObject prefab)
    {
        prefab.SetActive(false);
        npcProtectPool.Enqueue(prefab);
        /*ParticleSystem deadEffect = Instantiate(enemyDeadEffect, prefab.transform.position, Quaternion.identity);
        deadEffect.Play();*/
    }
}
