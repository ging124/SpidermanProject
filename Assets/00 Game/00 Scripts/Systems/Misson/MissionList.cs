using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/MissionList")]
public class MissionList : ScriptableObject
{
    public List<BaseMission> listMission = new List<BaseMission>();
    public int currentMissionIndex = 0;

    public GameEventListener<float, float> completeMission;
    public GameEvent changeProgressingMission;

    private void OnValidate()
    {
        currentMissionIndex = 0;
    }

    public void FinishedMission()
    {
        if (listMission.Count > 0)
        {
            if(currentMissionIndex == listMission.Count - 1)
            {
                currentMissionIndex = 0;
            }
            else
            {
                currentMissionIndex++;
            }
            changeProgressingMission.Raise();
        }
    }

    public void CheckFailedMission()
    {
        Debug.Log("Failed Mission");
    }

    public void InstantiateMission(Transform parent)
    {
        if (listMission[currentMissionIndex] != null)
        {
            listMission[currentMissionIndex].InstantiateMission(parent);
        }
    }

    public void UpdateMission(Transform parent)
    {

        if (listMission[currentMissionIndex] != null)
        {
            listMission[currentMissionIndex].UpdateMission(parent);
        }
    }
}
