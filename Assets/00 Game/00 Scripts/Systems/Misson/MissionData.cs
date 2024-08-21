using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/MissionData")]
public class MissionData : ScriptableObject
{
    public List<MissionDataConfig> missionDataConfigs = new List<MissionDataConfig>();

    [ContextMenu("GetMissionDataConfigs")]
    public void GetMissionDataConfigs()
    {
        float baseExp = 60;
        float baseMoney = 75;
        for (int i = 0; i < missionDataConfigs.Count; i++)
        {
            float value = 1;
            if (i + 1 <= 10)
            {
                value = 30;
            }
            else if (i + 1 is >= 10 and <= 30)
            {
                value = 10;
            }
            else
            {
                value = 5;
            }

            if (i == 0)
            {
                missionDataConfigs[i].expConfig = baseExp;
                missionDataConfigs[i].moneyConfig = baseMoney;
            }
            else
            {
                missionDataConfigs[i].expConfig = missionDataConfigs[i - 1].expConfig + value;
                missionDataConfigs[i].moneyConfig = missionDataConfigs[i - 1].moneyConfig + value;
            }
            
        }
    }
}

[Serializable]
public class MissionDataConfig
{
    public float expConfig;
    public float moneyConfig;
}

