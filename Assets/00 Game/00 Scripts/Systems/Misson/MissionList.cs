using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/MissionList")]
public class MissionList : ScriptableObject
{
    public int currentMissionIndex = 0;
    public List<BaseMission> listMission = new List<BaseMission>();

    public GameEventListener completeMission;
    public GameEvent changeProgressingMission;

    private void OnValidate()
    {
        currentMissionIndex = 0;
    }

    public void FinishedMission()
    {
        if (listMission.Count > 0)
        {
            currentMissionIndex++;
            changeProgressingMission.Raise();
        }
    }

    public void CheckFailedMission()
    {
        Debug.Log("Failed Mission");
    }

    public void InstantiateMission(Transform parent)
    {
        if (currentMissionIndex != listMission.Count)
        {
            listMission[currentMissionIndex].InstantiateMisison(parent);
        }
    }

    public void UpdateMission(Transform parent)
    {
        if (currentMissionIndex != listMission.Count)
        {
            listMission[currentMissionIndex].UpdateMission(parent);
        }
    }
}
