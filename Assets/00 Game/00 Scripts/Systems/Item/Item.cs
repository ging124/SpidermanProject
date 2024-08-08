using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract  class Item : ScriptableObject, IFactory
{
    public GameObject prefabItemWorld;
    public Sprite image;
    public Queue<GameObject> poolItemWorld = new Queue<GameObject>();

    public GameEvent<Item> changeItem;


    public virtual GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent = default)
    {
        if (poolItemWorld.Count > 0)
        {
            var itemWorld = poolItemWorld.Dequeue().GetComponent<ItemWorld>();
            itemWorld.transform.position = position;
            itemWorld.transform.rotation = rotation;
            itemWorld.itemData = this;
            itemWorld.transform.SetParent(parent);
            itemWorld.gameObject.SetActive(true);
            return itemWorld.gameObject;
        }
        else
        {
            var itemWorld = Instantiate(prefabItemWorld, position, rotation, parent);
            itemWorld.GetComponent<ItemWorld>().itemData = this;
            return itemWorld;
        }
    }

    public void Despawn(GameObject prefab)
    {
        prefab.SetActive(false);
        prefab.transform.SetParent(null);
        poolItemWorld.Enqueue(prefab);
    }
}
