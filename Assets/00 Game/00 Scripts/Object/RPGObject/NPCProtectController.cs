using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCProtectController : MonoBehaviour, IHitable
{
    public NPCProtect npcProtecData;
    [SerializeField] private float currentHP;
    [SerializeField] private ClipTransition _anim;
    [SerializeField] private AnimancerComponent animancer;

    private void Awake()
    {
        animancer = this.GetComponent<AnimancerComponent>();
        npcProtecData.LevelUp();
    }

    private void OnEnable()
    {
        animancer.Play(_anim);
        currentHP = npcProtecData.maxHP;
    }

    public bool CanHit()
    {
        return true;
    }

    public void OnHit(int hitDamage, AttackType attackType)
    {
        
    }
}
