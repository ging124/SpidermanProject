using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FightingMission : MissionSteps
{

    [Header("MissionData")]
    public List<Waves> waveListData;
    public float fightingMissionRange;

    [Header("MissionProgress")]
    public int enemies = 0;
    public int waves = 0;

    public override bool CheckCompleteStep()
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

    public override bool CheckFailedStep()
    {
        return true;
    }

    public override void InstantiateStep(Transform parent)
    {

        enemies = 0;
        waves = 0;

        SpawnWave(parent, waves);
    }

    public override void UpdateStep(Transform parent)
    {
        enemies++;

        if(enemies == waveListData[waves].enemies.Count && waves != waveListData.Count - 1)
        {
            waves++;
            SpawnWave(parent, waves);
        }

        if (CheckCompleteStep())
        {
            completeStep.Raise();
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
