using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/MissionManagerSO")]
public class MissionManagerSO : ScriptableObject
{
    public BaseMission progressingMission;
    public List<BaseMission> listMission = new List<BaseMission>();
    public Queue<BaseMission> holdingMission;

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
        }
    }

    public void CheckFailedMission()
    {
        Debug.Log("Failse Mission");
    }

    public void InstantiateMission(Transform parent)
    {
        if(progressingMission != null)
        {
            progressingMission.InstantiateMisison(parent);
        }
    }
}
