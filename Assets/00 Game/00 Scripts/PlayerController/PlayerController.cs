using Animancer;
using DamageNumbersPro;
using EasyCharacterMovement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

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
    public float inwardsOffset = 0.1f;
    public float upPointSphereRadius = 0.5f;
    public float upSphereCastHeight = 50f;

    public Vector3 forwardPoint;
    public float forwardPointDistance = 10f;
    public float backPOffset = 0.2f;
    public float floorDistance = 40f;

    public RaycastHit hitSurface;
    public Vector3 pointSetBack;
    public Vector3 upPoint;
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

        if (Physics.SphereCast(cam.transform.position, zipDetectionRange, cam.transform.forward, out RaycastHit hit, zipDetectionLength, wallLayer))
        {
            hitSurface = hit;
            pointSetBack = hit.point - hit.normal * inwardsOffset;
            if (Vector3.Dot(hit.normal, Vector3.up) <= 0.99f)
            {
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, zipDetectionLength, LayerMask.NameToLayer("Ground")))
                {
                    zipPoint = Vector3.zero;
                    //Debug.Log("Found ground");
                    if (zipIconImage.gameObject.activeSelf)
                    {
                        zipIconImage.gameObject.SetActive(false);
                    }
                }
                else
                {
                    //Debug.Log("FaceZipPoint");
                    FaceZipPoint();
                }
            }
            else
            {
                //Debug.Log("FaceZipDownPoint");
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

        /*if (Physics.SphereCast(this.cam.position, this.zipDetectionRange, this.cam.forward, out this.zipPointDetection, this.zipDetectionLength, this.wallLayer))
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
        }*/


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

    private bool IsZipPointInView(Vector3 point)
    {
        Vector3 screenPoint = cam.GetComponent<Camera>().WorldToViewportPoint(point);
        return screenPoint.z > 0 && screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }

    private void FaceZipPoint()
    {
        var direc = Vector3.ProjectOnPlane(Vector3.up, hitSurface.normal);
        if (Physics.Raycast(pointSetBack, direc, out RaycastHit hit2, zipDetectionLength, wallLayer))
        {
            upPoint = hit2.point + Vector3.up * upSphereCastHeight;
            //Debug.DrawRay(upPoint, -hit2.transform.up, Color.blue);
            if (Physics.SphereCast(upPoint, upPointSphereRadius, -hit2.transform.up, out RaycastHit hit3, upSphereCastHeight, wallLayer))
            {
                //Debug.DrawRay(hit3.point, hit3.normal, Color.magenta);
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
        forwardPoint = pointSetBack + direc.normalized * forwardPointDistance;
        //var distanceFD = Vector3.Distance(direc, pointSetBack);
        //Debug.DrawLine(pointSetBack, forwardPoint, Color.red);

        #region Tutruocvao

        if (Physics.Raycast(forwardPoint, -direc, out RaycastHit hit2, floorDistance, wallLayer))
        {
            //Debug.Log("hit2 - FaceDownZipPoint");
            var hit2Point = hit2.point + -hit2.normal * backPOffset;
            if (Physics.Raycast(hit2Point, Vector3.up, out RaycastHit hit3, zipDetectionLength, wallLayer))
            {
                //Debug.DrawLine(hit2Point, hit2Point + Vector3.up, Color.yellow);
                //Debug.Log("hit 3 - FaceDownZipPoint");

                var up = hit3.point + Vector3.up * 0.2f;
                if (Physics.Raycast(up, -direc, out RaycastHit hit4, 5, wallLayer))
                {
                    var back = hit4.point + -hit4.normal * backPOffset;
                    if (Physics.Raycast(back, Vector3.up, out RaycastHit hit5, 10, wallLayer))
                    {
                        if (Vector3.Angle(hit5.normal, Vector3.up) < 45)
                        {
                            zipPoint = hit5.point;
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
                        zipPoint = hit3.point;
                        ShowFocusZipPoint();
                    }
                }
                else if (Physics.Raycast(up, direc, out RaycastHit hit6, 5, wallLayer))
                {
                    //Debug.Log("hit6 - FaceDownZipPoint");
                    var forward = hit6.point + -hit6.normal * backPOffset;
                    if (Physics.Raycast(forward, Vector3.up, out RaycastHit hit7, 10, wallLayer))
                    {
                        if (Vector3.Angle(hit7.normal, Vector3.up) < 45)
                        {
                            zipPoint = hit7.point;
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
                        zipPoint = hit3.point;
                        ShowFocusZipPoint();
                    }
                }
                else
                {
                    zipPoint = hit3.point;
                    ShowFocusZipPoint();
                }
            }
            else
            {
                //Debug.Log("hit3 - FaceDownZipPoint - Null");
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
