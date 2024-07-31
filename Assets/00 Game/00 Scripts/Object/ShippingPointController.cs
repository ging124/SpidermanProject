using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippingPointController : MonoBehaviour
{
    public ShippingPoint shippingPoint;
    [SerializeField] GameEvent shippingDone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shippingDone.Raise();
            shippingPoint.Despawn(this.gameObject);
        }
    }
}
