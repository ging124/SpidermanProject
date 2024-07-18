using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDraw : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }
}
