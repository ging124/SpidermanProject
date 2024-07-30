using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] MissionManagerSO missionManagerSO;
    [SerializeField] GameEventListener gameEventListener;

    private void Awake()
    {
        missionManagerSO.InstantiateMission(this.transform);

        gameEventListener.Register();
    }

    private void OnDestroy()
    {
        gameEventListener.Unregister();

    }

    public void CheckMissionFinished()
    {
        missionManagerSO.FinishedMission();
    }

    private void CheckMissionFailed()
    {
        missionManagerSO.CheckFailedMission();
    }

}
