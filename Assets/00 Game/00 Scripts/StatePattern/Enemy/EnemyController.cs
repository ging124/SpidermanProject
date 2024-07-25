using Animancer;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public int currentHP, currentMP;

    public UnityEvent enemyActive;

    public Enemy enemyData;

    public PlayerController player;
    public CapsuleCollider capCollider;
    public Rigidbody rigid;
    public AnimancerComponent animancer;
    public EnemyBlackboard enemyBlackboard;
    public NavMeshAgent agent;

    private void Awake()
    {
        SetEnemyStatus();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        capCollider = this.GetComponent<CapsuleCollider>();
        rigid = this.GetComponent<Rigidbody>();
        animancer = this.GetComponent<AnimancerComponent>();
        enemyBlackboard = this.GetComponent<EnemyBlackboard>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    void SetEnemyStatus()
    {
        currentHP = enemyData.maxHP;
        currentMP = enemyData.maxMP;
    }

   /* void OnEnable()
    {
        SetEnemyStatus();
        enemyActive?.Invoke();
        this.onHit = false;
    }

    public void OnHit(int hitDamage)
    {
        enemyBlackboard.onHit = true;
        enemyBlackboard.hitDamage = hitDamage;
        StartCoroutine(SetOnHitFalse());
    }

    public IEnumerator SetOnHitFalse()
    {
        yield return new WaitForSeconds(0.2f);
        enemyBlackboard.onHit = false;
        enemyBlackboard.hitDamage = 0;
    }*/
}
