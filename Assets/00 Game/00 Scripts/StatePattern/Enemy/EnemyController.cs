using Animancer;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyController : ObjectController, IHitable
{
    public GameEvent enemyDead;
    public GameEvent<Enemy> updateQuestProgress;

    public float currentHP;

    public bool canAttack;
    public bool followPlayer;

    public Enemy enemyData;

    public PlayerController player;
    public CapsuleCollider capCollider;
    public Rigidbody rigid;
    public AnimancerComponent animancer;
    public EnemyBlackboard enemyBlackboard;
    public NavMeshAgent agent;

    private void Awake()
    {
        currentHP = enemyData.maxHP.Value;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        capCollider = this.GetComponent<CapsuleCollider>();
        rigid = this.GetComponent<Rigidbody>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    public void FollowPlayer()
    {
        if (!followPlayer)
        {
            followPlayer = true;
        }
    }
}
