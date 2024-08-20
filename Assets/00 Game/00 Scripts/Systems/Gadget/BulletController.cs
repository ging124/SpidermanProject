using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : ItemWorld
{
    [SerializeField] protected float flyDuration = 0.3f;

    protected virtual void OnEnable()
    {
        Shoot();
    }

    protected void DestroyBullet()
    {
        itemData.Despawn(this.gameObject);
    }

    public virtual void Shoot()
    {
    }
}
