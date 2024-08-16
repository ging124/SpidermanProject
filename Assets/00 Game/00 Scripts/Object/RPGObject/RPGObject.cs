using System;
using UnityEngine;

[Serializable]
public abstract class RPGObject : ScriptableObject
{
    public LevelSystem levelSystem;
    public float maxHP;
    public float attackDamage;
    public float moveSpeed;

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
