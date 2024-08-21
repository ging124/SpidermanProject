using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Barracuda;
using Unity.VisualScripting;
using UnityEngine;

public class BaseMission : ScriptableObject
{
    [Header("MissionReward")]
    public float expReward;
    public float moneyReward;

    public MissionData missionData;
    private float expBase = 50;
    private float moneyBase = 75;


    [Header("CreateMission")]
    public Vector3 spawnPosition;

    [Header("ChangeStepEvent")]
    public GameEvent<float, float> completeMission;

    [ContextMenu("RewardData")]
    private void RewardData()
    {
        float missionBonus;
        if(this.GetType () == typeof(FightingBossMission))
        {
            missionBonus = 1.75f;
        }
        else
        {
            missionBonus = 1.5f;
        }
        int index;
        int.TryParse(Regex.Match(this.name, @"\d+").Value, out index);
        expReward = MissionReward(missionData.missionDataConfigs[index - 1].expConfig, missionBonus, index, expBase);
        moneyReward = MissionReward(missionData.missionDataConfigs[index - 1].moneyConfig, missionBonus, index, moneyBase);
    }

    public int MissionReward(float missionConfig, float missionBonus, float index, float baseValue)
    {
        return Mathf.RoundToInt(missionConfig * missionBonus + Mathf.Log(index * baseValue, 2));
    }

    public virtual bool CheckCompleteMission()
    {
        return true;
    }

    public virtual bool CheckCompleteMission(int objectCount)
    {
        return true;
    }

    public virtual bool CheckFailedMission()
    {
        return false;
    }

    public virtual void InstantiateMission(Transform parent)
    {
    }

    public virtual void UpdateMission(Transform parent)
    {

    }

    public virtual void GetMissionPossition(Transform pos)
    {
        spawnPosition = pos.position;
    }
}
