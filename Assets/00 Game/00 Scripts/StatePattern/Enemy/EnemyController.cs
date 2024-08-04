using Animancer;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : ObjectController, IHitable
{
    public GameEvent enemyDead;

    public float currentHP;

    public Enemy enemyData;
    public EnemyManager enemyManager;

    public bool followPlayer;
    public bool canAttack;

    public PlayerController player;
    public CapsuleCollider capCollider;
    public Rigidbody rigid;
    public AnimancerComponent animancer;
    public EnemyBlackboard enemyBlackboard;
    public NavMeshAgent agent;
    public UIEnemyBlackboard uIEnemyBlackboard;

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
        uIEnemyBlackboard.enemyHPBar.EnemyHPChange(currentHP, enemyData.maxHP.Value);
        enemyManager.Add(enemyData);
    }

    private void OnDisable()
    {
        enemyManager.Remove(enemyData);
    }
}
