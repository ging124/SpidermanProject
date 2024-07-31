using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Object/ShippingPoint")]
public class ShippingPoint : ScriptableObject, IFactory
{
    public GameObject shippingPointPrefab;
    Queue<GameObject> shippingPointPool = new Queue<GameObject>();

    public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (shippingPointPool.Count > 0)
        {
            var shippingPoint = shippingPointPool.Dequeue().GetComponent<ShippingPointController>();
            shippingPoint.transform.position = position;
            shippingPoint.transform.rotation = rotation;
            shippingPoint.transform.SetParent(parent);
            shippingPoint.gameObject.SetActive(true);
            return shippingPoint.gameObject;
        }
        else
        {
            var shippingPoint = Instantiate(shippingPointPrefab, position, rotation, parent);
            shippingPoint.GetComponent<ShippingPointController>().shippingPoint = this;
            return shippingPoint;
        }
    }

    public void Despawn(GameObject prefab)
    {
        prefab.SetActive(false);
        shippingPointPool.Enqueue(prefab);
    }
}
