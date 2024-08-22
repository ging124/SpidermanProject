using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShippingPointPosition
{
    public Vector3 position;
    [SerializeField] ShippingPoint shippingPoint;

    public GameObject SpawnPoint(Transform parent)
    {
        return shippingPoint.Spawn(position, Quaternion.identity, parent);
    }
}
