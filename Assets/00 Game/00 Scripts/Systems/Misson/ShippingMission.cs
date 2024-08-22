using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/ShippingMission")]
public class ShippingMission : BaseMission
{
    [Header("MissionData")]
    public GetShippingPoint getShippingMission;
    public GameObject getShippingMisisonObject;
    public List<ShippingPointPosition> listShippingPointPosition;
    public List<GameObject> listShippingPointsObject;

    public float timeRequired;
    public GameEvent<ShippingMission> shippingMissionUI;


    [Header("MissionProgress")]
    public int point = 0;

    public override bool CheckCompleteMission()
    {
        if (point == listShippingPointPosition.Count + 1)
        {
            shippingMissionUI.Raise(this);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void MissionFailed()
    {
        if(getShippingMisisonObject.activeInHierarchy)
        {
            getShippingMission.Despawn(getShippingMisisonObject);
        }
        //foreach ()
    }

    public override void InstantiateMission(Transform parent)
    {
        point = 0;

        getShippingMisisonObject = getShippingMission.Spawn(spawnPosition, Quaternion.identity, parent);
    }

    public override void UpdateMission(Transform parent)
    {
        point++;

        if (point == 1)
        {
            SpawnShippingPoint(parent);
            shippingMissionUI.Raise(this);
        }

        if (CheckCompleteMission())
        {
            completeMission.Raise(moneyReward, expReward);
        }

    }

    public void SpawnShippingPoint(Transform parent)
    {
        foreach (ShippingPointPosition shippingPoint in listShippingPointPosition)
        {
            listShippingPointsObject.Add(shippingPoint.SpawnPoint(parent));
        }
    }

    public override void GetMissionPossition(Transform pos)
    {
        base.GetMissionPossition(pos);

        int i = 0;
        foreach(Transform t in pos)
        {
            listShippingPointPosition[i].position = t.position;
            i++;
        }

        //EditorUtility.SetDirty(this);
    }
}
