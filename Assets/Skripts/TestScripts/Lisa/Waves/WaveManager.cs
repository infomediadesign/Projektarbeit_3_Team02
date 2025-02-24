using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    private List<WaveSpawner> waveSpawnerList = new List<WaveSpawner>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Debug.Log("WaveManager already exists", gameObject);

        }
    }

    public void AddWaveSpawner(WaveSpawner inWaveSpawner)
    {
        waveSpawnerList.Add(inWaveSpawner);
    }

    public void RemoveWaveSpawner(WaveSpawner inWaveSpawner)
    {
        waveSpawnerList.Remove(inWaveSpawner);
    }

    public List<WaveSpawner> GetWaveSpawnerList()
    {
        return waveSpawnerList;
    }

    public void SetWaveSpawnerList(List<WaveSpawner> inWaveSpawnerList)
    {
        this.waveSpawnerList = inWaveSpawnerList;
    }
}
