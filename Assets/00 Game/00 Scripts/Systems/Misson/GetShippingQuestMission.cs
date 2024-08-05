using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/GetShippingQuestMission")]
public class GetShippingQuestMission : MissionSteps
{
    [Header("MissionData")]
    public GetShippingPoint getShippingPoint;
    [SerializeField] Vector3 spawnPosition;


    [Header("MissionProgress")]
    public int point = 0;

    public override bool CheckCompleteStep()
    {
        if (point == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool CheckFailedStep()
    {
        return true;
    }

    public override void InstantiateStep(Vector3 position, Transform parent)
    {
        spawnPosition = position;

        point = 0;

        SpawnPoint(parent);
    }

    public override void UpdateStep(Transform parent)
    {
        point++;

        if (CheckCompleteStep())
        {
            completeStep.Raise();
        }

    }

    public void SpawnPoint(Transform parent)
    {
        Debug.Log("SpawnPoint");
        getShippingPoint.Spawn(spawnPosition, Quaternion.identity, parent);
    }
}
