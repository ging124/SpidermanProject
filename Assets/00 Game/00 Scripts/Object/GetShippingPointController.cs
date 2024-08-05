using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetShippingPointController : MonoBehaviour
{
    public GetShippingPoint getShippingPoint;
    [SerializeField] GameEvent getShippingDone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            getShippingDone?.Raise();
            getShippingPoint.Despawn(this.gameObject);
        }
    }

}
