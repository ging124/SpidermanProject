using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour, IHitable
{
    [Header("----Object Controller----")]
    public Vector3 movement;
    public bool onHit;
    public bool isDead;
    public int hitDamage;

    public void OnHit(int hitDamage)
    {
        if (isDead == true)
        {
            this.onHit = false;
            this.hitDamage = 0;
            StopCoroutine(SetOnHitFalse());
            return;
        }

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
