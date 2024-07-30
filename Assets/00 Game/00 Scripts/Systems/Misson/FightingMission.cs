using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/FightingMission")]
public class FightingMission : BaseMission
{
    public List<Waves> waveListData;
    public List<Waves> currentWaveList = new List<Waves>();
    public GameEventListener<Enemy> gameEventListener;

    private void OnEnable()
    {
        gameEventListener.Register();
        currentWaveList = waveListData;
    }
    private void OnDisable()
    {
        gameEventListener.Unregister();
    }

    public override bool CheckCompleteMission()
    {
        if (waveListData[currentWaveList.Count - 1].enemies.Count == 0)
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
        foreach (Waves wave in currentWaveList)
        {
            int randomRange = Random.Range(0, wave.enemies.Count);
            wave.SpawnWave(new Vector3(randomRange, 0, randomRange) + spawnPosition, parent);
        }
    }

    public void UpdateEnemyInMission(Enemy enemy)
    {
        foreach (Waves wave in currentWaveList)
        {
            wave.DespawnWave(enemy);
        }
    }
}
