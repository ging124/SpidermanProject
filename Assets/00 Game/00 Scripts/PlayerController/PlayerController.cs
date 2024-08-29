using Animancer;
using DamageNumbersPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : RPGObjectController
{
    [Header("----PlayerController----")]
    public float currentHP;
    public float ultimateRange;
    public DamageNumber damagePrefab;
    public DamageNumber missPrefab;
    public bool haveEnemyTarget;

    public LayerMask friendLayer;

    [Header("----Effect----")]
    public GameEffect attackHitEffect;
    public ParticleSystem spiderSence;


    [Header("----AttackValue----")]
    public float nearAttackRange;
    public float mediumAttackRange;
    public float farAttackRange;
    public bool canAttack;

    RaycastHit hit;
    RaycastHit hit2;

    [Header("----WallRunValue----")]
    public Transform wallRunPoint;
    public LayerMask wallLayer;
    public float detectionLength;
    public RaycastHit frontWallHit;

    public bool groundHit;


    [Header("----ZipValue----")]
    public LayerMask zipLayer;
    public float zipDetectionRange = 0.5f;
    public float zipDetectionLength = 35;
    public RaycastHit zipPointDetection;
    public Image zipIconImage;

    public float inwardsOffset = 0.1f;
    public float upPointSphereRadiusFace = 6f;
    public float upSphereCastHeightFace = 50f;
    public float upPointSphereRadiusTop = 0.6f;
    public float upSphereCastHeightTop = 20f;

    public float forwardPointDistance = 10f;
    public float backPOffset = 0.2f;

    public RaycastHit hitSurface;
    public Vector3 pointSetBack;
    public Vector3 upPoint;
    [HideInInspector] public float zipLength => (zipPoint - this.transform.position).magnitude;

    [Header("----ReadOnly----")]
    public bool onSwim;
    public bool wallFront;
    public Vector3 zipPoint;

    [Header("----Event----")]
    public UnityEvent<float, float> playerChangeHP;
    public UnityEvent hitCombo;
    public GameEvent playerDead;
    public GameEvent<ICountdownable> skillCountDown;

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
        canHit = true;
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

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(zipPoint, zipDetectionRange);
        Gizmos.color = Color.yellow;
        if(Physics.SphereCast(cam.transform.position, zipDetectionRange, cam.transform.forward, out RaycastHit hit, zipDetectionLength, zipLayer))
        {
            Gizmos.DrawSphere(hit.point, 1);
        }

        Gizmos.DrawSphere(hit2.point, 1);
    }*/

    private void Update()
    {
        WallCheck();
        GroundCheck();

        if(haveEnemyTarget)
        {
            spiderSence.Play();
        }
        else
        {
            spiderSence.Stop();
        }
    }

    public void WallCheck()
    {
        this.wallFront = Physics.Raycast(wallRunPoint.transform.position, this.transform.forward, out this.frontWallHit, this.detectionLength, this.wallLayer);
    }

    public void GroundCheck()
    {
        this.groundHit = Physics.Raycast(this.transform.position, -Vector3.up, 2, this.wallLayer);
    }

    public void ZipPointCheck()
    {
        if (Physics.SphereCast(cam.transform.position, zipDetectionRange, cam.transform.forward, out RaycastHit hit, zipDetectionLength, zipLayer))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                zipPoint = Vector3.zero;
                if (zipIconImage.gameObject.activeSelf)
                {
                    zipIconImage.gameObject.SetActive(false);
                }
                return;
            }

            hitSurface = hit;
            pointSetBack = hit.point - hit.normal * inwardsOffset;
            if (Vector3.Dot(hit.normal, Vector3.up) <= 0.99f)
            {
                FaceZipPoint();
            }
            else
            {
                FaceDownZipPoint();
            }
        }
        else
        {
            zipPoint = Vector3.zero;
            if (zipIconImage.gameObject.activeSelf)
            {
                zipIconImage.gameObject.SetActive(false);
            }
        }

        if (this.zipPoint != Vector3.zero)
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

    private bool IsZipPointInView(Vector3 point)
    {
        Vector3 screenPoint = cam.GetComponent<Camera>().WorldToViewportPoint(point);
        return screenPoint.z > 0 && screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }

    private void FaceZipPoint()
    {
        upPoint = pointSetBack + Vector3.up * upSphereCastHeightFace;
        if (Physics.SphereCast(upPoint, upPointSphereRadiusFace, Vector3.down, out RaycastHit hit3, upSphereCastHeightFace, wallLayer))
        {
            if (Vector3.Angle(hit3.normal, Vector3.up) < 45)
            {
                zipPoint = hit3.point;
                ShowFocusZipPoint();
            }
            else
            {
                zipPoint = Vector3.zero;
                if (zipIconImage.gameObject.activeSelf)
                {
                    zipIconImage.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            zipPoint = Vector3.zero;
            if (zipIconImage.gameObject.activeSelf)
            {
                zipIconImage.gameObject.SetActive(false);
            }
        }
    }

    private void FaceDownZipPoint()
    {
        var direc = Vector3.ProjectOnPlane(cam.transform.forward, hitSurface.normal);
        var direc1 = Vector3.ProjectOnPlane(-cam.transform.forward, hitSurface.normal);
        Vector3 forwardPoint = pointSetBack + direc.normalized * forwardPointDistance;
        Vector3 forwardPoint1 = pointSetBack + direc1.normalized * forwardPointDistance;
        #region Tutruocvao

        if (Physics.Raycast(forwardPoint, -direc, out hit2, upSphereCastHeightTop, wallLayer))
        {
            Vector3 upPoint = hit2.point + Vector3.up * upSphereCastHeightTop;
            Vector3 upPoint2 = this.transform.position + Vector3.up * upSphereCastHeightTop;

            if (Physics.SphereCast(upPoint, upPointSphereRadiusTop, Vector3.down, out RaycastHit hit3, zipDetectionLength, wallLayer)
                && !Physics.Raycast(upPoint2, direc, zipDetectionLength, wallLayer))
            {
                zipPoint = hit3.point;
                ShowFocusZipPoint();
            }
            else
            {
                zipPoint = Vector3.zero;
                if (zipIconImage.gameObject.activeSelf)
                {
                    zipIconImage.gameObject.SetActive(false);
                }
            }
        }
        else if (Physics.Raycast(forwardPoint1, -direc1, out hit2, upSphereCastHeightTop, wallLayer))
        {
            Vector3 upPoint = hit2.point + Vector3.up * upSphereCastHeightTop;
            Vector3 upPoint2 = this.transform.position + Vector3.up * upSphereCastHeightTop;

            if (Physics.SphereCast(upPoint, upPointSphereRadiusTop, Vector3.down, out RaycastHit hit3, zipDetectionLength, wallLayer)
                && !Physics.Raycast(upPoint2, direc, zipDetectionLength, wallLayer))
            {
                zipPoint = hit3.point;
                ShowFocusZipPoint();
            }
            else
            {
                zipPoint = Vector3.zero;
                if (zipIconImage.gameObject.activeSelf)
                {
                    zipIconImage.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            //Debug.Log("hit2 - FaceDownZipPoint null");
            zipPoint = Vector3.zero;
            if (zipIconImage.gameObject.activeSelf)
            {
                zipIconImage.gameObject.SetActive(false);
            }
        }

        #endregion
    }

    private void ShowFocusZipPoint()
    {
        Vector3 screenPoint = cam.GetComponent<Camera>().WorldToScreenPoint(zipPoint);
        if (zipIconImage != null)
        {
            zipIconImage.transform.position = screenPoint;
            if (zipIconImage.gameObject.activeSelf == false)
            {
                zipIconImage.gameObject.SetActive(true);
            }
        }
    }
}
