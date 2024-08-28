using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Reward/Chest")]
public class Chest : Item
{
    [SerializeField] private Player player;

    public GameEffect despawnEffect;

    public double xpCollectible;
    public double moneyCollectible;

    public void GetReward()
    {
        if (player.levelSystem.currentLevel <= 15)
        {
            xpCollectible = player.levelSystem.NextLevel(player.levelSystem.currentLevel) * (10 / 100f);
            moneyCollectible = xpCollectible + 50;
        }
        else
        {
            xpCollectible = player.levelSystem.NextLevel(player.levelSystem.currentLevel) * (10 / 100f);
            moneyCollectible = xpCollectible + 100;
        }

        xpCollectible = Mathf.RoundToInt((float)xpCollectible);
        moneyCollectible = Mathf.RoundToInt((float)moneyCollectible);

        player.levelSystem.GetExp(xpCollectible);
        player.money += (float)moneyCollectible;
    }
}
