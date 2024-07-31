using Animancer;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : ObjectController, IHitable
{
    public GameEvent enemyDead;

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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        capCollider = this.GetComponent<CapsuleCollider>();
        rigid = this.GetComponent<Rigidbody>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        currentHP = enemyData.maxHP.Value;
    }

    public void FollowPlayer()
    {
        if (!followPlayer)
        {
            followPlayer = true;
        }
    }
}
