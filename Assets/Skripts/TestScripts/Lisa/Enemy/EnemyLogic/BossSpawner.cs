using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject boss;
    private Transform player;    
    public float spawnDistance = 20f; 

    private bool bossSpawned = false;

    private void Awake()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }
    void Start()
    {
        boss.SetActive(false); 
    }

    void Update()
    {
        if (!boss.activeInHierarchy && Vector2.Distance(player.position, boss.transform.position) <= spawnDistance)
        {
            boss.SetActive(true); 
        }
    }
}

