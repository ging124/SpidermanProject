using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : ObjectController
{
    [Header("----InputValue----")]
    public LayerMask enemyLayer;


    [Header("----WallRunValue----")]
    public float attackRangeDetection;
    public float mediumAttackRange;
    public float farAttackRange;

    public Collider[] hitEnemy;
    public Collider enemyTarget;


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
    public Character character;
    public AnimancerComponent animancer;
    public Rigidbody rb;
    public Transform cam;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        playerData.currentHP = playerData.maxHP.Value;
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
        Gizmos.DrawWireSphere(this.transform.position, this.attackRangeDetection);
    }

    public void EnemyCheck()
    {
        hitEnemy = Physics.OverlapSphere(this.transform.position, this.attackRangeDetection, this.enemyLayer);

        if (hitEnemy.Length > 0)
        {
            float minDistance = float.MaxValue;
            int enemyFlag = -1;

            for (int i = 0; i < hitEnemy.Length; i++)
            {
                float distance = (hitEnemy[i].transform.position - this.transform.position).magnitude;
                Debug.DrawLine(hitEnemy[i].transform.position, this.transform.position, Color.black);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    enemyFlag = i;
                }
            }

            if (enemyFlag != -1) this.enemyTarget = hitEnemy[enemyFlag];
            else this.enemyTarget = null;
        }
        else
        {
            this.enemyTarget = null;
        }

    }

    public void WallCheck()
    {
        this.wallFront = Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y + this.character.GetHeight() / 2f, this.transform.position.z)
            , this.transform.forward, out this.frontWallHit, this.detectionLength, this.wallLayer);
    }

    public void ZipPointCheck()
    {
        if (Physics.SphereCast(this.cam.position, this.zipDetectionRange, this.cam.forward, out this.zipPointDetection, this.zipDetectionLength, this.wallLayer))
        {
            var wallScript = this.zipPointDetection.transform.GetComponent<WallScript>();
            this.zipPoint = wallScript.GetZipPoint(this.zipPointDetection.point);
        }
        else
        {
            this.zipPoint = Vector3.zero;
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
