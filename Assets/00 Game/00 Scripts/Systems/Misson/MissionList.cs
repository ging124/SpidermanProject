using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/MissionList")]
public class MissionList : ScriptableObject
{
    public BaseMission progressingMission;
    public List<BaseMission> listMission = new List<BaseMission>();
    public Queue<BaseMission> holdingMission;

    public GameEventListener completeMission;
    public GameEvent changeProgressingMission;

    private void OnValidate()
    {
        holdingMission = new Queue<BaseMission>(listMission);

        if (holdingMission.Count > 0) progressingMission = holdingMission.Dequeue();
    }

    public void FinishedMission()
    {
        if (holdingMission.Count > 0)
        {
            progressingMission = holdingMission.Dequeue();
            changeProgressingMission.Raise();
        }
    }

    public void CheckFailedMission()
    {
        Debug.Log("Failed Mission");
    }

    public void InstantiateMission(Transform parent)
    {
        if(progressingMission != null)
        {
            progressingMission.InstantiateMisison(parent);
        }
    }

    public void UpdateMission(Transform parent)
    {
        progressingMission.UpdateMission(parent);
    }
}
