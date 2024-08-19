using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : BulletController
{
    public PlayerController target;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Shoot()
    {
        Tweener tweener = this.transform.DOMove(this.transform.position + this.transform.forward * 15, flyDuration);
        tweener.onComplete = DestroyBullet;
        tweener.onUpdate = BulletOnHit;
    }

    public void BulletOnHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 0.5f))
        {
            hit.transform.GetComponent<PlayerController>().OnHit(10, AttackType.NormalAttack);
            DestroyBullet();
        }
    }
}
