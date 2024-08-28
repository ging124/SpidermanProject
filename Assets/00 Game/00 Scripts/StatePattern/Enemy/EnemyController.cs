using Animancer;
using DamageNumbersPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : RPGObjectController, IHitable
{
    public GameEvent enemyDead;
    public LayerMask enemyLayer;
    public Collider[] hitTarget;
    public DamageNumber damagePrefab;
    public DamageNumber missPrefab;

    public EnemyGroupBehaviour groupBehaviour;

    public float stunLockDuration = 0;

    public GameEffect attackHitEffect;

    public float currentHP;
    public Enemy enemyData;

    public bool canAttack;

    public CharacterController characterController;
    public AnimancerComponent animancer;
    public NavMeshAgent agent;
    public UIEnemyBlackboard uIEnemyBlackboard;

    protected override void Awake()
    {
        base.Awake();
        characterController = this.GetComponent<CharacterController>();
        animancer = this.GetComponent<AnimancerComponent>();
        agent = this.GetComponent<NavMeshAgent>();
        
    }

    protected virtual void OnEnable()
    {
        enemyData.levelSystem.currentLevel = enemyData.player.levelSystem.currentLevel;
        enemyData.LevelUp();
        uIEnemyBlackboard.enemyHPBar.SetLevelUI(enemyData.levelSystem.currentLevel);

        uIEnemyBlackboard.enemyHPBar.EnemyHPChange(currentHP, enemyData.maxHP);
        groupBehaviour.RegisterEnemy(this);
        canHit = true;
    }

    private void OnDisable()
    {
        groupBehaviour.UnregisterEnemy(this);
    }

    public override void TargetDetection()
    {
        Collider[] hitTarget = Physics.OverlapSphere(this.transform.position, this.rangeDetection, ~enemyLayer);

        if (hitTarget.Length > 0)
        {
            float minDistance = float.MaxValue;
            int tagetFlag = -1;

            for (int i = 0; i < hitTarget.Length; i++)
            {
                if (hitTarget[i].gameObject == this.gameObject)
                {
                    continue;
                }

                IHitable hitable;
                if (hitTarget[i].TryGetComponent<IHitable>(out hitable))
                {
                    float distance = (hitTarget[i].transform.position - this.transform.position).magnitude;
                    Debug.DrawLine(hitTarget[i].transform.position, this.transform.position, Color.black);
                    if (minDistance > distance && hitable.CanHit())
                    {
                        minDistance = distance;
                        tagetFlag = i;
                    }
                }
                else
                {
                    continue;
                }
            }

            if (tagetFlag != -1) this.target = hitTarget[tagetFlag];
            else this.target = null;
        }
        else
        {
            this.target = null;
        }
    }
}
