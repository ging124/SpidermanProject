using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_MissionComplete : UIBase
{
    public Player player;

    public TMP_Text expText;
    public TMP_Text moneyText;
    public float expReward;
    public float moneyReward;

    public GameEventListener<float, float> missionComplete;

    protected override void Awake()
    {
        base.Awake();
        missionComplete.Register();
    }

    protected void OnDestroy()
    {
        missionComplete.Unregister();
    }

    public void Reward(int multiple)
    {
        player.levelSystem.GetExp(expReward * multiple);
        player.money += moneyReward * multiple;
        this.CloseUI();
        expReward = 0;
        moneyReward = 0;
    }

    public void CompleteMission(float expReward, float moneyReward)
    {
        expText.text = expReward.ToString();
        moneyText.text = moneyReward.ToString();
        this.expReward = expReward;
        this.moneyReward = moneyReward;
        this.OpenUI();
    }
}
