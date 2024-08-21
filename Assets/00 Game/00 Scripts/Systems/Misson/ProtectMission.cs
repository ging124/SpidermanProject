using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/ProtectMission")]
public class ProtectMission : BaseMission
{
    [Header("MissionData")]
    public GetShippingPoint getFightingMission;
    public List<Waves> waveListData;
    public NPCProtect npcProtect;
    public NPCProtectController npcProtectController;
    public float fightingMissionRange;

    [Header("MissionProgress")]
    public int getMissionPoint = 0;
    public int enemies = 0;
    public int waves = 0;

    public override bool CheckCompleteMission()
    {
        if (waves != waveListData.Count - 1) return false;

        if (enemies == waveListData[waves].enemies.Count)
        {
            npcProtectController.npcProtecData.Despawn(npcProtectController.gameObject);
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
        getMissionPoint = 0;
        enemies = 0;
        waves = 0;

        getFightingMission.Spawn(spawnPosition, Quaternion.identity, parent);
    }

    public override void UpdateMission(Transform parent)
    {
        if (getMissionPoint == 0 && waves == 0)
        {
            npcProtectController = npcProtect.Spawn(spawnPosition + Vector3.up, Quaternion.identity, parent).GetComponent<NPCProtectController>();
            SpawnWave(parent, waves);
            getMissionPoint++;
            return;
        }

        enemies++;

        if (enemies == waveListData[waves].enemies.Count && waves != waveListData.Count - 1)
        {
            waves++;
            enemies = 0;
            SpawnWave(parent, waves);
        }

        if (CheckCompleteMission())
        {
            completeMission.Raise(moneyReward, expReward);
        }
    }

    public void SpawnWave(Transform parent, int waves)
    {
        foreach (Enemy enemyes in waveListData[waves].enemies)
        {
            float randomRange = UnityEngine.Random.Range(0, fightingMissionRange);
            enemyes.Spawn(new Vector3(randomRange, 0, randomRange) + spawnPosition, Quaternion.identity, parent);
        }
    }
}
