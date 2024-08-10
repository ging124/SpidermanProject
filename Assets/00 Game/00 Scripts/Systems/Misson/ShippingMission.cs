using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/ShippingMission")]
public class ShippingMission : BaseMission
{
    [Header("MissionData")]
    public GetShippingPoint getShippingMission;
    public List<ShippingPointPosition> listShippingPointPosition;

    [Header("MissionProgress")]
    public int point = 0;

    public override bool CheckCompleteMission()
    {
        if (point == listShippingPointPosition.Count + 1)
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

        getShippingMission.Spawn(spawnPosition, Quaternion.identity, parent);
    }

    public override void UpdateMission(Transform parent)
    {
        point++;

        if (point == 1)
        {
            SpawnShippingPoint(parent);
        }

        if (CheckCompleteMission())
        {
            completeMission.Raise();
        }

    }

    public void SpawnShippingPoint(Transform parent)
    {
        foreach (ShippingPointPosition shippingPoint in listShippingPointPosition)
        {
            shippingPoint.SpawnPoint(parent);
        }
    }
}
