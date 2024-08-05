using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/MissionList")]
public class MissionList : ScriptableObject
{
    public int currentMissionIndex = 0;
    public List<BaseMission> listMission = new List<BaseMission>();

    public GameEventListener completeMission;
    public GameEvent changeProgressingMission;
    public GameEvent changeProgressingStep;

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

    public void FinishedStep()
    {
        if (listMission[currentMissionIndex].missionSteps.Count > 0)
        {
            listMission[currentMissionIndex].currentStepsIndex++;
            changeProgressingStep.Raise();
        }
    }

    public void CheckFailedMission()
    {
        Debug.Log("Failed Mission");
    }

    public void CheckFailedStep()
    {
        Debug.Log("Failed Stept");
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

    public void UpdateStep(Transform parent)
    {
        if (listMission[currentMissionIndex].currentStepsIndex != listMission[currentMissionIndex].missionSteps.Count)
        {
            Debug.Log("Debug");
            var step = listMission[currentMissionIndex].currentStepsIndex;
            listMission[currentMissionIndex].missionSteps[step].UpdateStep(parent);
        }
    }
}
