using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/ShippingMission")]
public class ShippingMission : BaseMission
{
    [Header("MissionData")]
    public List<ShippingPointPosition> listShippingPointPosition;

    [Header("MissionProgress")]
    public int point = 0;

    public override bool CheckCompleteMission()
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

    public override bool CheckFailedMission()
    {
        return true;
    }

    public override void InstantiateMisison(Transform parent)
    {
        point = 0;

        SpawnWave(parent);
    }

    public override void UpdateMission(Transform parent)
    {
        point++;

        if (CheckCompleteMission())
        {
            completeMisison.Raise();
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
