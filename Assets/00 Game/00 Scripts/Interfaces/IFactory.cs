using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory
{
    public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent);
    public void Despawn(GameObject prefab);
}
