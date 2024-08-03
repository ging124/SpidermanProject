using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour, IHitable
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
        yield return 0;
        this.onHit = false;
        this.hitDamage = 0;
    }
}
