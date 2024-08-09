using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/ShippingMission")]
public class ShippingMission : BaseMission
{
    [Header("MissionData")]
    public GetShippingPoint getShippingPointPosition;
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

    public override void InstantiateMission(Transform parent)
    {
        point = 0;

        SpawnWave(parent);
    }

    public override void UpdateMission(Transform parent)
    {
        point++;
        Debug.Log("Update Mission");

        if (CheckCompleteMission())
        {
            completeMission.Raise();
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
