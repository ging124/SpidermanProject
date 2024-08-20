using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WebBulletController : BulletController
{
    public EnemyController target;
    public float stunLockDuration;

    public override void Shoot()
    {
        if (target != null)
        {
            this.transform.DOMove(target.transform.position, flyDuration).onComplete = () =>
            {
                target.stunLockDuration = this.stunLockDuration; 
                DestroyBullet();
            };
        }
        else
        {
            this.transform.DOMove(this.transform.position + this.transform.forward * 10, flyDuration).onComplete = () =>
            {
                DestroyBullet();
            };
        }
    }
    
}
