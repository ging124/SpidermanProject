using Animancer;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Skill/Boss/VenomSkill1")]
public class VenomSkill1 : Skill
{
    public override void UseSkill(Transform transform, AnimancerComponent animancer, float skillDamage, Collider target = null)
    {
        base.UseSkill(transform, animancer, skillDamage, target);
        animancer.Play(skillAnim);
    }

    public void StartAttack()
    {
        target.transform.position = transform.position + transform.forward;
        Attack(AttackType.StartGrabAttack);
    }

    public void NormalAttack()
    {
        Attack(AttackType.NormalAttack);
    }

    public void LastAttack()
    {
        Attack(AttackType.EndGrabAttack);
    }

    public override bool CanSkill(Collider target, Transform transform)
    {
        if (target == null) return false;

        if (Vector3.Distance(target.transform.position, transform.position) < skillRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
