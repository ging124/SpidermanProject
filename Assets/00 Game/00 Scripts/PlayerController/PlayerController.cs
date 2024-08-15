using Animancer;
using DamageNumbersPro;
using EasyCharacterMovement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : RPGObjectController
{
    [Header("----PlayerController----")]
    public float currentHP;
    public float ultimateRange;
    public DamageNumber damagePrefab;

    [Header("----Effect----")]
    public GameEffect attackHitEffect;

    [Header("----AttackValue----")]
    public float nearAttackRange;
    public float mediumAttackRange;
    public float farAttackRange;

    RaycastHit hit;

    [Header("----WallRunValue----")]
    public Transform wallRunPoint;
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

    [Header("----Event----")]
    public UnityEvent<float, float> playerChangeHP;
    public UnityEvent<double ,double, double> playerGainExp;


    [Header("----Component----")]
    public Player playerData;
    public AnimancerComponent animancer;
    public Rigidbody rb;
    public Transform cam;

    protected override void Awake()
    {
        base.Awake();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();
        animancer = this.GetComponent<AnimancerComponent>();
    }

    private void OnEnable()
    {
        currentHP = playerData.maxHP;
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
        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(hit.transform.position, 1);

        Gizmos.DrawWireSphere(this.transform.position, ultimateRange);
    }

    public void WallCheck()
    {
        this.wallFront = Physics.Raycast(wallRunPoint.transform.position, this.transform.forward, out this.frontWallHit, this.detectionLength, this.wallLayer);

        Debug.DrawRay(wallRunPoint.transform.position, this.transform.forward, Color.blue);
    }

    public void ZipPointCheck()
    {
        if (Physics.SphereCast(this.cam.position, this.zipDetectionRange, this.cam.forward, out this.zipPointDetection, this.zipDetectionLength, this.wallLayer))
        {
            WallScript wallScript;
            if (this.zipPointDetection.transform.TryGetComponent<WallScript>(out wallScript))
            {
                this.zipPoint = wallScript.GetZipPoint(this.zipPointDetection.point);
            }
            else
            {
                this.zipPoint = Vector3.zero;
            }
        }
        if (this.zipPoint != Vector3.zero && this.zipLength < this.maxZipLength)
        {
            this.zipIconImage.gameObject.SetActive(true);
            Camera camera = this.cam.GetComponent<Camera>();
            this.zipIconImage.transform.position = camera.WorldToScreenPoint(this.zipPoint);
        }
        else
        {
            this.zipIconImage.gameObject.SetActive(false);
        }
    }

    public void SetLineRenderer(LineRenderer lineRenderer, Transform hand, Vector3 zipPoint)
    {
        lineRenderer.SetPosition(0, hand.position);
        lineRenderer.SetPosition(1, zipPoint);
    }

}
