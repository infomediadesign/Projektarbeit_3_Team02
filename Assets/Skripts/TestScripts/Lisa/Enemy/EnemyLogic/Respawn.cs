using System.Collections;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public float respawnTime = 3f; 

    private Vector3 spawnPosition;

    void Start()
    {
        spawnPosition = transform.position; 
    }

    private void OnDestroy()
    {

        StartCoroutine(RespawnEnemy());
    }

    IEnumerator RespawnEnemy()
    {
        yield return new WaitForSeconds(respawnTime);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
