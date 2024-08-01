using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] MissionList missionManagerSO;
    [SerializeField] GameEventListener updateMissionEvent;
    [SerializeField] GameEventListener changeMissionEvent;

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
}
