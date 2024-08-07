using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WebBulletController : ItemWorld
{
    public Rigidbody rigid;
    public float flySpeed;

    private void Awake()
    {
        rigid = this.GetComponent<Rigidbody>();
        Invoke(nameof(DestroyBullet), 10);
    }

    private void Update()
    {
        rigid.MovePosition(this.transform.position + this.transform.forward * flySpeed * Time.deltaTime);
    }

    private void DestroyBullet()
    {
        itemData.Despawn(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        IHitable hitable;
        if (other.gameObject.TryGetComponent<IHitable>(out hitable))
        {
            hitable.OnHit(5);
            DestroyBullet();
        }
    }
}
