using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/FightingMission")]
public class FightingMission : BaseMission
{
    [Header("MissionData")]
    public GetShippingPoint getFightingMission;
    public List<Waves> waveListData;
    public float fightingMissionRange;

    [Header("MissionProgress")]
    public int getMisisonPoint = 0;
    public int enemies = 0;
    public int waves = 0;

    public override bool CheckCompleteMission()
    {
        if(waves != waveListData.Count - 1) return false;

        int enemyCount = 0;
        foreach (Waves wave in waveListData)
        {
            enemyCount += wave.enemies.Count;
        }

        if (enemies == enemyCount)
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
        getMisisonPoint = 0;
        enemies = 0;
        waves = 0;

        getFightingMission.Spawn(spawnPosition, Quaternion.identity, parent);
    }

    public override void UpdateMission(Transform parent)
    {
        if (getMisisonPoint == 0) getMisisonPoint++;
        if(getMisisonPoint == 1 && waves == 0)
        {
            SpawnWave(parent, waves);
            enemies++;
        }

        if(enemies == waveListData[waves].enemies.Count && waves != waveListData.Count - 1)
        {
            waves++;
            SpawnWave(parent, waves);
        }

        if (CheckCompleteMission())
        {
            completeMission.Raise();
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
