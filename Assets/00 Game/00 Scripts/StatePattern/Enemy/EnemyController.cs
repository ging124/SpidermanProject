using Animancer;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : RPGObjectController, IHitable
{
    public GameEvent enemyDead;

    public float stunLockDuration = 0;

    public float currentHP;
    public Enemy enemyData;
    public EnemyManager enemyManager;

    public bool canAttack;

    public Rigidbody rigid;
    public AnimancerComponent animancer;
    public NavMeshAgent agent;
    public UIEnemyBlackboard uIEnemyBlackboard;

    protected override void Awake()
    {
        base.Awake();
        rigid = this.GetComponent<Rigidbody>();
        animancer = this.GetComponent<AnimancerComponent>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    protected virtual void OnEnable()
    {
        uIEnemyBlackboard.enemyHPBar.EnemyHPChange(currentHP, enemyData.maxHP);
        //enemyManager.Add(enemyData);
    }

    /*private void OnDisable()
    {
        enemyManager.Remove(enemyData);
    }*/
}
