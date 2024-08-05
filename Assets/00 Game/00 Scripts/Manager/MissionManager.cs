using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] MissionList missionManagerSO;
    [SerializeField] GameEventListener updateStepEvent;
    [SerializeField] GameEventListener changeMissionEvent;

    private void Awake()
    {
        InstantiateMission();
        updateStepEvent.Register();
        changeMissionEvent.Register();
    }

    private void OnDestroy()
    {
        updateStepEvent.Unregister();
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

    public void UpdateStep()
    {
        missionManagerSO.UpdateStep(this.transform);
    }
}
