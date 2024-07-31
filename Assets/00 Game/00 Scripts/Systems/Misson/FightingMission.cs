using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/FightingMission")]
public class FightingMission : BaseMission
{
    [Header("MissionData")]
    public List<Waves> waveListData;

    [Header("MissionProgress")]
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

    public override void InstantiateMisison(Transform parent)
    {
        enemies = 0;
        waves = 0;

        SpawnWave(parent, waves);
    }

    public override void UpdateMission(Transform parent)
    {
        enemies++;

        if(enemies == waveListData[waves].enemies.Count && waves != waveListData.Count - 1)
        {
            waves++;
            SpawnWave(parent, waves);
        }

        if (CheckCompleteMission())
        {
            completeMisison.Raise();
        }

    }

    public void SpawnWave(Transform parent, int waves)
    {
        foreach (Enemy enemyes in waveListData[waves].enemies)
        {
            float randomRange = Random.Range(0, detectRangeMission);
            enemyes.Spawn(new Vector3(randomRange, 0, randomRange) + spawnPosition, Quaternion.identity, parent);
        }
    }
}
