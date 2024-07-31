using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FightingMission : BaseMission
{
<<<<<<< HEAD
    public List<Waves> currentWaveList = new List<Waves>();
    public List<Waves> waveListData = new List<Waves>();
    public GameEventListener<Enemy> gameEventListener;

=======
    [Header("MissionData")]
    public List<Waves> waveListData;

    [Header("MissionProgress")]
    public int enemies = 0;
    public int waves = 0;

>>>>>>> d17e3a924d28d42dcc3cb290fb0af7f4e9a42e29
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
<<<<<<< HEAD
        foreach (Waves wave in currentWaveList)
        {
            int randomRange = UnityEngine.Random.Range(0, wave.enemies.Count);
            wave.SpawnWave(new Vector3(randomRange, 0, randomRange) + spawnPosition, parent);
        }
=======
        enemies = 0;
        waves = 0;

        SpawnWave(parent, waves);
>>>>>>> d17e3a924d28d42dcc3cb290fb0af7f4e9a42e29
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
