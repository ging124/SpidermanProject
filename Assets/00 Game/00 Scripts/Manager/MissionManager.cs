using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] MissionList missionManagerSO;
    
    [SerializeField] GameEventListener updateMissionEvent;
    [SerializeField] GameEventListener changeMissionEvent;

    [Header("Mission Location Data")]
    [SerializeField] Transform missionLocation;

    private void Awake()
    {
        InstantiateMission();
        updateMissionEvent.Register();
        changeMissionEvent.Register();
    }

    private void OnDestroy()
    {
        updateMissionEvent.Unregister();
        changeMissionEvent.Unregister();
    }

    public void InstantiateMission()
    {
        missionManagerSO.InstantiateMission(this.transform);
    }

    private void CheckMissionFailed()
    {
        missionManagerSO.CheckFailedMission();
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
            missionManagerSO.listMission[i].spawnPosition = t.position;
            i++;
        }
    }
}
