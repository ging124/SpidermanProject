using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseMission
{
    [Header("CreateMission")]
    public Vector3 spawnPosition;

    [Header("ChangeStepEvent")]
    public GameEvent completeStep;

    public List<MissionSteps> missionSteps = new List<MissionSteps>();
    public int currentStepsIndex = 0;

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
        if (currentStepsIndex != missionSteps.Count)
        {
            missionSteps[currentStepsIndex].InstantiateStep(spawnPosition, parent);
        }
    }

    public virtual void UpdateMission(Transform parent)
    {
        if (currentStepsIndex != missionSteps.Count)
        {
            missionSteps[currentStepsIndex].UpdateStep(parent);
        }
    }
}
