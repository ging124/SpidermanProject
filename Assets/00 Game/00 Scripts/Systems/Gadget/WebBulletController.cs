using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBulletController : ItemWorld
{
    public Vector3 target;
    public Rigidbody rigid;
    public float flySpeed;

    private void Awake()
    {
        rigid = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rigid.MovePosition(this.transform.position + this.transform.forward * flySpeed * Time.deltaTime);
    }

    void Shoot()
    {
        if(target != Vector3.zero)
        {
            transform.LookAt(target);
            transform.position = Vector3.Lerp(transform.position, target, 0.2f);
        }
        else
        {
            rigid.AddForce(this.transform.forward);
        }
    }
}
