using Animancer;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Skill/Boss/VenomSkill1")]
public class VenomSkill1 : Skill
{
    public override void UseSkill(Transform transform, AnimancerComponent animancer, Collider target, float skillDamage)
    {
        base.UseSkill(transform, animancer, target, skillDamage);
        
    }

    public void MoveToTarget()
    {
        transform.DOMove(target.transform.position, 0.2f);
    }

    public virtual void Attack()
    {
        if (target == null) return;

        var targetComponent = target.GetComponent<IHitable>();

        var damage = transform.GetComponent<RPGObject>().RandomDamage(this.skillDamage);
        targetComponent.OnHit(damage, AttackType.NormalAttack);
    }

    public virtual void LastAttack()
    {
        if (target == null) return;

        var targetComponent = target.GetComponent<IHitable>();

        var damage = transform.GetComponent<RPGObject>().RandomDamage(this.skillDamage);
        targetComponent.OnHit(damage, AttackType.HeavyAttack);
    }
}
