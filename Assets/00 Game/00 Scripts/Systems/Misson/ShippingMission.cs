using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/ShippingMission")]
public class ShippingMission : MissionSteps
{
    [Header("MissionData")]
    public List<ShippingPointPosition> listShippingPointPosition;

    [Header("MissionProgress")]
    public int point = 0;

    public override bool CheckCompleteStep()
    {
        if (point == listShippingPointPosition.Count)
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
        point = 0;

        SpawnWave(parent);
    }

    public override void UpdateStep(Transform parent)
    {
        point++;

        if (CheckCompleteStep())
        {
            completeStep.Raise();
        }

    }

    public void SpawnWave(Transform parent)
    {
        foreach (ShippingPointPosition shippingPoint in listShippingPointPosition)
        {
            shippingPoint.SpawnPoint(parent);
        }
    }
}
