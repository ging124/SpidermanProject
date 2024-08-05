using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Object/GetShippingPointQuest")]
public class GetShippingPoint : ScriptableObject, IFactory
{
    public GameObject getShippingPointPrefab;
    Queue<GameObject> getShippingPointPool = new Queue<GameObject>();

    public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (getShippingPointPool.Count > 0)
        {
            var shippingPoint = getShippingPointPool.Dequeue().GetComponent<GetShippingPointController>();
            shippingPoint.transform.position = position;
            shippingPoint.transform.rotation = rotation;
            shippingPoint.transform.SetParent(parent);
            shippingPoint.gameObject.SetActive(true);
            return shippingPoint.gameObject;
        }
        else
        {
            var shippingPoint = Instantiate(getShippingPointPrefab, position, rotation, parent);
            shippingPoint.GetComponent<GetShippingPointController>().getShippingPoint = this;
            return shippingPoint;
        }
    }

    public void Despawn(GameObject prefab)
    {
        prefab.SetActive(false);
        getShippingPointPool.Enqueue(prefab);
    }
}
