using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseMission : ScriptableObject
{
    public Vector3 spawnPosition;
    public float detectRangeMission;

    public float moneyReward;
    public float expReward;


    public virtual bool CheckCompleteMission()
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
}
