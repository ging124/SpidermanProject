using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Reward/Chess")]
public class Chess : Item
{
    [SerializeField] private Player player;

    public void GetReward()
    {
        double xpCollectible;
        double moneyCollectible;
        if (player.levelSystem.currentLevel <= 15)
        {
            xpCollectible = player.levelSystem.NextLevel(player.levelSystem.currentLevel) * 10 / 100;
            moneyCollectible = xpCollectible + 50;
        }
        else
        {
            xpCollectible = player.levelSystem.NextLevel(player.levelSystem.currentLevel) * 10 / 100;
            moneyCollectible = xpCollectible + 100;
        }

        player.levelSystem.GetExp(xpCollectible);
        player.money += (float)moneyCollectible;
    }
}
