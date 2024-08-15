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

    public virtual void UseSkill(Transform transform, AnimancerComponent animancer, Collider target, float skillDamage)
    {
        this.transform = transform;
        this.animancer = animancer;
        this.target = target;
        this.skillDamage = skillDamage;
    }
}
