using System;
using UnityEngine;

[Serializable]
public abstract class RPGObject : ScriptableObject
{
    public LevelSystem levelSystem;
    public float maxHP;
    public float attackDamage;

    public float baseMaxHP;
    public float baseAttackDamage;
    public float moveSpeed;

    public float hpUpLevel;
    public float dmgUpLevel;

    public GameEventListener levelup;

    public void LevelUp()
    {
        if(levelSystem.currentLevel != 1)
        {
            maxHP = baseMaxHP + (float)(levelSystem.currentLevel * hpUpLevel);
            attackDamage = baseAttackDamage + (float)(levelSystem.currentLevel * dmgUpLevel);
        }
        else
        {
            maxHP = baseMaxHP;
            attackDamage = baseAttackDamage;
        }
    }

    public static int RandomDamage(float damage, float randomDamageRange = 0.2f)
    {
        float minRandomDamage = damage - damage * randomDamageRange;
        float maxRandomDamage = damage + damage * randomDamageRange;
        if (minRandomDamage <= 0)
        {
            minRandomDamage = 1;
        }
        int randomDamage = (int)UnityEngine.Random.Range(minRandomDamage, maxRandomDamage + 1);
        return randomDamage;
    }
}
