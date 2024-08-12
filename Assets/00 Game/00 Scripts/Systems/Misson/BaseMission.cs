using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseMission : ScriptableObject
{
    [Header("CreateMission")]
    public Vector3 spawnPosition;

    [Header("ChangeStepEvent")]
    public GameEvent completeMission;

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
