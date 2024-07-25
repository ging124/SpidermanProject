using Animancer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyController : ObjectController
{
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
        enemyData.SetHpStart();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        capCollider = this.GetComponent<CapsuleCollider>();
        rigid = this.GetComponent<Rigidbody>();
        animancer = this.GetComponent<AnimancerComponent>();
        enemyBlackboard = this.GetComponent<EnemyBlackboard>();
        agent = this.GetComponent<NavMeshAgent>();
    }
}
