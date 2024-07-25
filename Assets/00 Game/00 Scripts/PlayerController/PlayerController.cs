using Animancer;
using EasyCharacterMovement;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int maxHP, maxMP;
    public int currentHP, currentMP;
    public int armor;
    public int attackDamage;

    public Vector3 movement;

    public bool onHit;
    public int hitDamage;

    public LayerMask wallLayer;
    public Transform swingPointOnCam;
    public float detectionLength;
    public RaycastHit frontWallHit;

    public float zipDetectionRange;
    public float zipDetectionLength;
    public float zipLength => (zipPoint - this.transform.position).magnitude;
    public float maxZipLength = 50;
    public RaycastHit zipPointDetection;
    public Vector3 zipPoint;
    public Image zipIconImage;

    [Header("----ReadOnly----")]
    public bool onSwim;
    public bool wallFront;


    public Player playerData;

    [Header("----Component----")]
    public AnimancerComponent animancer;
    public Rigidbody rb;
    public Transform cam;
    public PlayerBlackboard blackboard;

    void Awake()
    {
        animancer = this.GetComponent<AnimancerComponent>();
        rb = this.GetComponent<Rigidbody>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        blackboard = this.GetComponent<PlayerBlackboard>();
    }

    public void OnHit(int hitDamage)
    {
        this.onHit = true;
        this.hitDamage = hitDamage;
        StartCoroutine(SetOnHitFalse());
    }

    public IEnumerator SetOnHitFalse()
    {
        yield return new WaitForSeconds(0.2f);
        this.onHit = false;
        this.hitDamage = 0;
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
