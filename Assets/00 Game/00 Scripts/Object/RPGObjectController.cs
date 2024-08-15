using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGObjectController : MonoBehaviour, IHitable
{
    [Header("----Object Controller----")]
    public Vector3 movement;
    public bool canHit;
    public int hitDamage;
    public float rangeDetection;
    public AttackType hitAttackType;
    public Collider target;


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

    public void TargetDetection()
    {
        Collider[] hitTarget = Physics.OverlapSphere(this.transform.position, this.rangeDetection);

        if (hitTarget.Length > 0)
        {
            float minDistance = float.MaxValue;
            int tagetFlag = -1;

            for (int i = 0; i < hitTarget.Length; i++)
            {
                if (hitTarget[i].gameObject == this.gameObject)
                {
                    continue;
                }

                IHitable hitable;
                if (hitTarget[i].TryGetComponent<IHitable>(out hitable))
                {
                    float distance = (hitTarget[i].transform.position - this.transform.position).magnitude;
                    Debug.DrawLine(hitTarget[i].transform.position, this.transform.position, Color.black);
                    if (minDistance > distance && hitable.CanHit())
                    {
                        minDistance = distance;
                        tagetFlag = i;
                    }
                }
                else
                {
                    continue;
                }
            }

            if (tagetFlag != -1) this.target = hitTarget[tagetFlag];
            else this.target = null;
        }
        else
        {
            this.target = null;
        }
    }
}
