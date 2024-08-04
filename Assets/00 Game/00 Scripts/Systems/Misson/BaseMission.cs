using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseMission
{
    [Header("CreateMission")]
    public Vector3 spawnPosition;
    public float detectRangeMission;

    [Header("RewardMission")]
    public float moneyReward;
    public float expReward;

    [Header("ChangeMissionEvent")]
    public GameEvent completeMisison;

    public List<MissionSteps> missionSteps = new List<MissionSteps>();
    public int currentMissionStepsIndex = 0;

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

    public virtual void InstantiateMisison(Transform parent)
    {
    }

    public virtual void UpdateMission(Transform parent)
    {

    }
}
