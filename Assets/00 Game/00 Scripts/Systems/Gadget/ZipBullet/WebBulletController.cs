using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WebBulletController : ItemWorld
{
    public EnemyController target;
    public float stunLockDuration;
    private float flyDuration = 0.3f;


    private void OnEnable()
    {
        Shoot();
    }

    private void DestroyBullet()
    {
        itemData.Despawn(this.gameObject);
    }

    private void Shoot()
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
