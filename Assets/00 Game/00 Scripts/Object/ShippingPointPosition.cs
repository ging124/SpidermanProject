using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShippingPointPosition
{
    public Vector3 position;
    [SerializeField] ShippingPoint shippingPoint;

    public void SpawnPoint(Transform parent)
    {
        shippingPoint.Spawn(position, Quaternion.identity, parent);
    }
}
