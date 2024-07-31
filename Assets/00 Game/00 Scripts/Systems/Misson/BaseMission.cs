using UnityEngine;


public class BaseMission : ScriptableObject
{
    [Header("CreateMission")]
    public Vector3 spawnPosition;
    public float detectRangeMission;

    [Header("RewardMission")]
    public float moneyReward;
    public float expReward;

    [Header("ChangeMissionEvent")]
    public GameEvent completeMisison;


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
