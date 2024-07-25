using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Animancer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyBlackboard : Blackboard
{
    public bool followPlayer;
    public bool canAttack;

    public UnityEvent followPlayerEvent;

    public Enemy enemyData;
    public EnemyController enemyController;
    public PlayerController player;
    public CapsuleCollider capCollider;
    public Rigidbody rigid;
    public AnimancerComponent animancer;
    public NavMeshAgent agent;
    
    public void FollowPlayer()
    {
        if(!followPlayer)
        {
            followPlayer = true;
            followPlayerEvent?.Invoke();
        }
    }
}
