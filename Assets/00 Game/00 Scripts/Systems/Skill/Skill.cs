using Animancer;
using UnityEngine;

public class Skill : ScriptableObject
{
    public ClipTransition skillAnim;
    protected Transform transform;
    protected Collider target;
    protected float skillDamage;
    protected AnimancerComponent animancer;
    public AttackType attackType;
    public float distanceToAttack;
    public GameEffect effect;

    public virtual void UseSkill(Transform transform, AnimancerComponent animancer, Collider target, float skillDamage)
    {
        this.transform = transform;
        this.animancer = animancer;
        this.target = target;
        this.skillDamage = skillDamage;

    }

    public virtual void Attack(AttackType attackType)
    {
        var targetComponent = target.GetComponent<IHitable>();

        var damage = RPGObject.RandomDamage(this.skillDamage);

        if (Vector3.Distance(this.target.transform.position, this.transform.position) < 1.5f)
        {
            effect.Spawn(this.target.transform.position, Quaternion.identity, null);
            targetComponent.OnHit(damage, attackType);
        }
    }

    public virtual bool CanSkill(Collider target, Transform transform)
    {
        return false;
    }
}
