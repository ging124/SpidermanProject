using Animancer;
using EasyCharacterMovement;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : ObjectController
{
    [Header("----InputValue----")]
    public LayerMask enemyLayer;

    public Transform leftLeg;
    public Transform rightLeg;
    public Transform leftHand;
    public Transform rightHand;

    [Header("----WallRunValue----")]
    public LayerMask wallLayer;
    public float detectionLength;
    public RaycastHit frontWallHit;

    [Header("----ZipValue----")]
    public float zipDetectionRange;
    public float zipDetectionLength;
    public float maxZipLength = 50;
    public RaycastHit zipPointDetection;
    public Image zipIconImage;
    public float zipLength => (zipPoint - this.transform.position).magnitude;

    [Header("----ReadOnly----")]
    public bool onSwim;
    public bool wallFront;
    public Vector3 zipPoint;

    [Header("----Component----")]
    public Player playerData;
    public Animator anim;
    public AnimancerComponent animancer;
    public Rigidbody rb;
    public Transform cam;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        playerData.SetHpStart();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            onSwim = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            onSwim = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(zipPointDetection.point, zipDetectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(zipPoint, zipDetectionRange);
    }
}
