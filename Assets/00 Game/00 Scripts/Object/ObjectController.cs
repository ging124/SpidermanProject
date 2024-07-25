using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [Header("----Object Controller----")]
    public Vector3 movement;
    public bool onHit;
    public int hitDamage;

    public void OnHit(int hitDamage)
    {
        this.onHit = true;
        this.hitDamage = hitDamage;
        StartCoroutine(SetOnHitFalse());
    }

    public IEnumerator SetOnHitFalse()
    {
        yield return new WaitForSeconds(0.2f);
        this.onHit = false;
        this.hitDamage = 0;
    }
}
