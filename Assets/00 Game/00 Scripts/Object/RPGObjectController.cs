using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGObjectController : MonoBehaviour, IHitable
{
    [Header("----Object Controller----")]
    public Vector3 movement;
    public bool canHit;
    public int hitDamage;
    public AttackType hitAttackType;

    protected virtual void Awake()
    {
        canHit = true;
    }

    public void OnHit(int hitDamage, AttackType attackType)
    {
        if (!CanHit())
        {
            this.hitDamage = 0;
            this.hitAttackType = AttackType.None;
            StopCoroutine(SetOnHitFalse());
            return;
        }

        this.hitAttackType = attackType;
        this.hitDamage = hitDamage;
        StartCoroutine(SetOnHitFalse());
    }

    public bool CanHit()
    {
        return canHit;
    }

    public IEnumerator SetOnHitFalse()
    {
        yield return 0;
        this.hitAttackType = AttackType.None;
        this.hitDamage = 0;
    }
}
