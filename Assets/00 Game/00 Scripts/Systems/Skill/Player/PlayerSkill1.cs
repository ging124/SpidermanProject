using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Skill/Player/PlayerSkill1")]
public class PlayerSkill1 : Skill, ICountdownable
{
    public float skillCountdown;
    public LayerMask friendLayer;
    public GameEffect skillEffect;
    private GameObject skillEffectObject;
    public bool canUseSkill = true;

    public override void UseSkill(Transform transform, AnimancerComponent animancer, float skillDamage, Collider target = null)
    {
        base.UseSkill(transform, animancer, skillDamage, target);

        animancer.Play(skillAnim, 0.25f, FadeMode.FromStart);
    }

    public void Attack()
    {
        Attack(attackType);
    }

    public void DespawnSkillEffect()
    {
        skillEffect.Despawn(skillEffectObject);
    }

    public override void Attack(AttackType attackType)
    {
        skillEffectObject = skillEffect.Spawn(transform.position + Vector3.up, transform.rotation);
        Collider[] ultimateTarget = Physics.OverlapSphere(transform.position, skillRange, ~friendLayer);

        foreach (Collider hit in ultimateTarget)
        {
            if (hit.gameObject == transform.gameObject) continue;

            IHitable hitable;
            if (hit.TryGetComponent<IHitable>(out hitable))
            {
                var damage = RPGObject.RandomDamage(skillDamage * 3);

                hitable.OnHit(damage, attackType);

                hitEffect.Spawn(hit.transform.position, Quaternion.identity, null);
            }
        }
    }

    public float TimmerCountdown()
    {
        return skillCountdown;
    }
}
