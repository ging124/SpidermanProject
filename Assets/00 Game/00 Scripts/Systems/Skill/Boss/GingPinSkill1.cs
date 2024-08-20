using Animancer;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Skill/Boss/GingPinSkill")]
public class GingPinSkill1 : Skill
{
    [SerializeField] ClipTransition startSkillAnim;
    [SerializeField] ClipTransition stopSkillAnim;

    public override void UseSkill(Transform transform, AnimancerComponent animancer, Collider target, float skillDamage)
    {
        base.UseSkill(transform, animancer, target, skillDamage);
        this.animancer.Play(startSkillAnim).Events.OnEnd = Skill;
    }

    public void NormalAttack()
    {
        Attack(AttackType.HeavyAttack);
    }

    public override bool CanSkill(Collider target, Transform transform)
    {
        if (target != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Skill()
    {
        animancer.Play(skillAnim);
        Vector3 movePos = target.transform.position + (target.transform.position - transform.position).normalized * 4;
        movePos.y = transform.position.y;
        this.transform.DOLookAt(target.transform.position, 0.3f, AxisConstraint.Y);
        Tweener tweener = this.transform.DOMove(movePos, movePos.magnitude / 150f);
        tweener.OnUpdate(NormalAttack);
        tweener.OnComplete(() =>
        {
            this.animancer.Play(stopSkillAnim);
        });
    }

    public override void Attack(AttackType attackType)
    {
        var targetComponent = target.GetComponent<IHitable>();

        var damage = RPGObject.RandomDamage(this.skillDamage);

        if (Vector3.Distance(this.target.transform.position, this.transform.position) < 1.5f)
        {
            targetComponent.OnHit(damage, attackType);
        }
    }
}
