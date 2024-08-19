using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LoginReward : UIBase
{
    public Transform contents;
    public Transform claimButton;

    public List<UIRewardItem> listUIReward = new List<UIRewardItem>();
    public List<RewardItem> rewards = new List<RewardItem>();
    public int indexCurrentReward;
    public int lastDay;

    private void Start()
    {
        lastDay = PlayerPrefs.GetInt("LastDay");
        listUIReward[indexCurrentReward].FocusReward();
    }

    public void OnEnable()
    {
        if (lastDay != System.DateTime.Now.Day || lastDay == 0)
        {
            claimButton.gameObject.SetActive(true);
        }
    }

    [ContextMenu("GetRewardData")]
    public void GetRewardData()
    {
        listUIReward.Clear();

        int i = 0;
        foreach (Transform content in contents)
        {
            var uiReward = content.GetComponent<UIRewardItem>();
            listUIReward.Add(uiReward);
            uiReward.rewardItem = rewards[i];
            uiReward.SetReward();
            i++;
        }
    }

    public void ClaimButton()
    {
        lastDay = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastDay", lastDay);
        listUIReward[indexCurrentReward].CompleteReward();
        indexCurrentReward++;
        if (indexCurrentReward < listUIReward.Count)
        {
            listUIReward[indexCurrentReward].FocusReward();
        }
        claimButton.gameObject.SetActive(false);
    }
}
