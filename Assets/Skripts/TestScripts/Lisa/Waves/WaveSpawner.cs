using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject spawnPoint;
    public float startTime;
    public float endTime;
    public float spawnRate;


    void Start()
    {
        WaveManager.instance.AddWaveSpawner(this);

        InvokeRepeating("Spawn", startTime, spawnRate);
        Invoke("CancelSpawn", endTime);
    }
    void Spawn()
    {
        if (projectilePrefab != null)
        {
            Debug.Log("Spawning started.");
            Vector3 spawnPosition = spawnPoint.transform.position;
            spawnPosition.z = 0f;
            GameObject spawnedObject = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Prefab to spawn is not assigned.");
        }
    }
    void CancelSpawn()
    {
        CancelInvoke("Spawn");
        WaveManager.instance.GetWaveSpawnerList().Remove(this);

        Debug.Log("Spawning cancelled.");
    }

    void OnDestroy()
    {
        WaveManager.instance.GetWaveSpawnerList().Remove(this);
    }
}
