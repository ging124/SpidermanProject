using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : BulletController
{
    public PlayerController target;
    public LayerMask playerLayer;
    public Tweener tweener;

    protected override void OnEnable()
    {
    }

    public override void Shoot()
    {
        tweener = this.transform.DOMove(this.transform.position + this.transform.forward * 15, flyDuration);
        tweener.onUpdate = BulletOnHit;
        tweener.onComplete = DestroyBullet;
    }

    public void BulletOnHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 1, playerLayer))
        {
            hit.transform.GetComponent<PlayerController>().OnHit(10, AttackType.NormalAttack);
            Debug.Log("DealDamage");
            tweener.Kill();
            DestroyBullet();
        }
    }
}
