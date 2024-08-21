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
    public LayerMask friendLayer;

    [Header("----Effect----")]
    public GameEffect attackHitEffect;

    [Header("----AttackValue----")]
    public float nearAttackRange;
    public float mediumAttackRange;
    public float farAttackRange;
    public bool canAttack;

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
    public GameEvent playerDead;

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
        playerData.LevelUp();
    }

    private void OnEnable()
    {
        canAttack = true;
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
            if (zipPointDetection.normal == Vector3.up)
            {
                Debug.Log("Mai Nha");
            }
            else
            {
                RaycastHit wallHit;
                RaycastHit zipHit;

                if (Physics.Raycast(zipPointDetection.point - zipPointDetection.normal * 0.2f, Vector3.ProjectOnPlane(Vector3.up, zipPointDetection.normal), out wallHit))
                {
                    if(Physics.Raycast(wallHit.point + Vector3.up * 5, -wallHit.transform.up, out zipHit))
                    {
                        Debug.DrawLine(zipHit.point, zipHit.normal, Color.red);
                        this.zipPoint = zipHit.point;
                    }
                }
                Debug.DrawRay(zipPointDetection.point - zipPointDetection.normal * 0.2f, Vector3.ProjectOnPlane(Vector3.up, zipPointDetection.normal), Color.red);
            }
        }
        else
        {
            zipPoint = Vector3.zero;
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

    public override void TargetDetection()
    {
        Collider[] hitTarget = Physics.OverlapSphere(this.transform.position, this.rangeDetection, ~friendLayer);

        if (hitTarget.Length > 0)
        {
            float minDistance = float.MaxValue;
            int tagetFlag = -1;

            for (int i = 0; i < hitTarget.Length; i++)
            {
                if (hitTarget[i].gameObject == this.gameObject)
                {
                    continue;
                }

                IHitable hitable;
                if (hitTarget[i].TryGetComponent<IHitable>(out hitable))
                {
                    float distance = (hitTarget[i].transform.position - this.transform.position).magnitude;
                    Debug.DrawLine(hitTarget[i].transform.position, this.transform.position, Color.black);
                    if (minDistance > distance && hitable.CanHit())
                    {
                        minDistance = distance;
                        tagetFlag = i;
                    }
                }
                else
                {
                    continue;
                }
            }

            if (tagetFlag != -1) this.target = hitTarget[tagetFlag];
            else this.target = null;
        }
        else
        {
            this.target = null;
        }
    }

}
