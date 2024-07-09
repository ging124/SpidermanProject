using Animancer;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHP, maxMP;
    public int currentHP, currentMP;
    public int armor;
    public int attackDamage;

    public Player playerData;

    [Header("----ReadOnly----")]
    public Vector3 slopeVelocity;
    public float groundedCheckDis;
    public bool onGround;
    public bool onSlope;
    public LayerMask groundLayer;

    [Header("----Component----")]
    public AnimancerComponent animancer;
    public CharacterController charController;
    public Transform cam;
    public Blackboard blackboard;

    void Awake()
    {
        GetData();
        SetStatus();
        animancer = this.GetComponent<AnimancerComponent>();
        charController = this.GetComponent<CharacterController>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        blackboard = this.GetComponent<Blackboard>();
        blackboard.SetBlackboard(currentHP, currentMP ,playerData, this, animancer, charController, cam);
    }

    void GetData()
    {
        this.maxHP = playerData.maxHP;
        this.maxMP = playerData.maxMP;
        this.armor = playerData.armor;
        this.attackDamage = playerData.attackDamage;
    }

    void SetStatus()
    {
        this.currentHP = maxHP;
        this.currentMP = maxMP;
    }

    void Update()
    {
        CheckGround();
        SetSlopeSlideVelocity();
        CheckSlope();
        AddGravity();
    }

    public void CheckGround()
    {
        groundedCheckDis = (charController.height / 2) + playerData.bufferCheckDis;

        if (Physics.SphereCast(this.transform.position, charController.radius,-this.transform.up, out RaycastHit hit, groundedCheckDis, groundLayer))
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

        blackboard.onGround = onGround;
    }

    void AddGravity()
    {
        if (onGround && blackboard.velocity.y < 0)
        {
            blackboard.velocity.y = 0;
        }

        blackboard.velocity.y += playerData.gravityValue * -9.81f * Time.deltaTime;
        charController.Move(new Vector3(0, blackboard.velocity.y * Time.deltaTime, 0));
    }

    void SetSlopeSlideVelocity()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfor, groundedCheckDis + 0.6f, groundLayer))
        {
            float angle = Vector3.Angle(hitInfor.normal, Vector3.up);

            if (angle >= charController.slopeLimit)
            {
                slopeVelocity = Vector3.ProjectOnPlane(new Vector3(0, playerData.gravityValue, 0), hitInfor.normal);
                blackboard.slopeVelocity = slopeVelocity;
                return;
            }
        }

        slopeVelocity = Vector3.zero;
        blackboard.slopeVelocity = slopeVelocity;
    }

    void CheckSlope()
    {
        if (slopeVelocity == Vector3.zero)
        {
            onSlope = false;
        }
        else
        {
            onSlope = true;
        }

        blackboard.onSlope = onSlope;
    }

    public void OnHit(int hitDamage)
    {
        blackboard.onHit = true;
        blackboard.hitDamage = hitDamage;
        StartCoroutine(SetOnHitFalse());
    }

    public IEnumerator SetOnHitFalse()
    {
        yield return new WaitForSeconds(0.2f);
        blackboard.onHit = false;
        blackboard.hitDamage = 0;
    }
}
