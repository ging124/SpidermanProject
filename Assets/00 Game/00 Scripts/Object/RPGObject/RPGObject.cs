using System;
using UnityEngine;

[Serializable]
public abstract class RPGObject : ScriptableObject
{
    public float currentHP;
    public LevelSystem levelSystem;
    public FloatVariables maxHP;
    public FloatVariables attackDamage;
    public FloatVariables moveSpeed;

    [SerializeField] private float _randomDamageRange = 0.2f;

    public void OnEnable()
    {
        currentHP = maxHP.Value;
    }

    public int RandomDamage(float damage)
    {
        float minRandomDamage = damage - damage * _randomDamageRange;
        float maxRandomDamage = damage + damage * _randomDamageRange;
        if (minRandomDamage <= 0)
        {
            minRandomDamage = 1;
        }
        int randomDamage = (int)UnityEngine.Random.Range(minRandomDamage, maxRandomDamage + 1);
        return randomDamage;
    }
}
