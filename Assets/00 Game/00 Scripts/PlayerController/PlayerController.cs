using Animancer;
using EasyCharacterMovement;
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

    [Header("----Component----")]
    public AnimancerComponent animancer;
    public Rigidbody rb;
    public Character character;
    public Transform cam;
    public Blackboard blackboard;

    void Awake()
    {
        GetData();
        SetStatus();
        animancer = this.GetComponent<AnimancerComponent>();
        character = this.GetComponent<Character>();
        rb = this.GetComponent<Rigidbody>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        blackboard = this.GetComponent<Blackboard>();
        blackboard.SetBlackboard(currentHP, currentMP ,playerData, this, animancer, character, cam, rb);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            blackboard.onSwim = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            blackboard.onSwim = false;
        }
    }
}
