using System;
using UnityEngine;

[Serializable]
public class GetShippingPointPosition
{
    [SerializeField] Vector3 position;
    [SerializeField] GetShippingPoint getShippingPoint;

    public void SpawnPoint(Transform parent)
    {
        getShippingPoint.Spawn(position, Quaternion.identity, parent);
    }

}
