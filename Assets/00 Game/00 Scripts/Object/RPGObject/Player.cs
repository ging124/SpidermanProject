using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RPGObject/Player")]
public class Player : RPGObject
{
    public float jumpHeight;
    public float money;

    public GameObject playerPrefab;
    Queue<GameObject> playerPool = new Queue<GameObject>();

    public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (playerPool.Count > 0)
        {
            var player = this.playerPool.Dequeue().GetComponent<PlayerController>();
            player.transform.position = position;
            player.transform.rotation = rotation;
            player.transform.SetParent(parent);
            player.gameObject.SetActive(true);
            return player.gameObject;
        }
        else
        {
            var player = Instantiate(playerPrefab, position, rotation, parent);
            player.GetComponent<PlayerController>().playerData = this;
            player.gameObject.SetActive(true);
            return player;
        }
    }

    public void Despawn(GameObject prefab)
    {
        prefab.SetActive(false);
        playerPool.Enqueue(prefab);
        /*ParticleSystem deadEffect = Instantiate(enemyDeadEffect, prefab.transform.position, Quaternion.identity);
        deadEffect.Play();*/
    }

}
