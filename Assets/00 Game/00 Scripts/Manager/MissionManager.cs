using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionManager : MonoBehaviour
{
    [SerializeField] MissionList missionManagerSO;

    [SerializeField] GameEventListener updateMissionEvent;
    [SerializeField] GameEventListener changeMissionEvent;
    [SerializeField] GameEventListener missionFailed;

    [Header("Mission Location Data")]
    [SerializeField] Transform missionLocation;

    private void Awake()
    {
        InstantiateMission();
        updateMissionEvent.Register();
        changeMissionEvent.Register();
        missionFailed.Register();
    }

    private void OnDestroy()
    {
        updateMissionEvent.Unregister();
        changeMissionEvent.Unregister();
        missionFailed.Unregister();

    }

    public void InstantiateMission()
    {
        missionManagerSO.InstantiateMission(this.transform);
    }

    public void MissionFailed()
    {
        missionManagerSO.MissionFailed();
    }

    public void UpdateMission()
    {
        missionManagerSO.UpdateMission(this.transform);
    }


    [ContextMenu("GetMissionListLocation")]
    public void GetMissionListLocation()
    {
        int i = 0;
        foreach (Transform t in missionLocation)
        {
            missionManagerSO.listMission[i].GetMissionPossition(t);
            i++;
        }
    }
}
