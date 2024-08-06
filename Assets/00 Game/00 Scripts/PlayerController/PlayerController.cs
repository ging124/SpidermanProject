using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class PlayerController : ObjectController
{
    [Header("----PlayerController----")]
    public LayerMask enemyLayer;
    public float currentHP;

    [Header("----Effect----")]
    public ParticleSystem ultimateSkill;


    [Header("----Model----")]
    public Transform modelHolder;
    public Skin currentSkin;
    public Transform leftHand;
    public Transform rightHand;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("----AttackValue----")]
    public float attackRangeDetection;
    public float nearAttackRange;
    public float mediumAttackRange;
    public float farAttackRange;

    public Collider[] hitEnemy;
    public Collider enemyTarget;
    RaycastHit hit;

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

    [Header("----GameEvent----")]
    [SerializeField] private GameEventListener<Item> changeSkin; 


    [Header("----Component----")]
    public Player playerData;
    public Character character;
    public AnimancerComponent animancer;
    public Rigidbody rb;
    public Transform cam;

    void Awake()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();
        animancer = this.GetComponent<AnimancerComponent>();
        currentHP = playerData.maxHP.Value;
        InnitSkin();
    }

    private void OnEnable()
    {
        changeSkin.Register();
    }

    private void OnDisable()
    {
        changeSkin.Unregister();
    }

    void InnitSkin()
    {
        var currentModel= modelHolder.GetComponentInChildren<SkinController>();
        var skin = currentSkin.Spawn(this.transform.position, this.transform.rotation, modelHolder);

        animancer.Animator.avatar = currentSkin.avatar;
        skinnedMeshRenderer = skin.GetComponentInChildren<SkinnedMeshRenderer>();
        Transform[] bones = skinnedMeshRenderer.bones;

        foreach (Transform bone in bones)
        {
            if (bone.name.ToLower().Contains("lefthand"))
            {
                leftHand = bone.transform;
            }

            if (bone.name.ToLower().Contains("righthand"))
            {
                rightHand = bone.transform;
            }

            if (bone.name.ToLower().Contains("hand_l"))
            {
                leftHand = bone.transform;
            }

            if (bone.name.ToLower().Contains("hand_r"))
            {
                rightHand = bone.transform;
            }
        }
    }

    public void ChangeSkin(Item item)
    {
        var skin = modelHolder.GetComponentInChildren<SkinController>();
        currentSkin.Despawn(skin.gameObject);
        currentSkin = (Skin)item;
        InnitSkin();
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
        Gizmos.DrawSphere(hit.transform.position, 1);
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

        /*if (Physics.SphereCast(transform.position, 1, movement.normalized, out hit, attackRangeDetection, enemyLayer))
        {
            if (hit.transform.GetComponent<IHitable>() != null)
            {
                enemyTarget = hit.collider;
            }
        }*/

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
