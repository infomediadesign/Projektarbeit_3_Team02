using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class Boss : EnemyBase
{
    private int phaseCount = 0;
    public GameObject normalMissilePrefab;
    public GameObject homingMissilePrefab;
    public Transform shootPoint;
    public float fireCooldown = 3f;
    private float fireCooldownTimer = 0f;
    private int destroyedWeaknesses = 0;
    private bool spawned = false;
    private Transform player;
    public float thornSpeed = 2f;
    public float thornRange = 5f;

    public static bool bossActive;
    private bool platformSpawned;

    public GameObject platformPrefab;
    public GameObject weaknessPrefab;
    public GameObject breakableWallPrefab;
    public GameObject lifePrefab;
    public GameObject thornObstaclePrefab;

    public Transform[] spawnPointPlatformConst;
    public Transform[] spawnPointsPlatformPhase1;
    public Transform[] spawnPointsWeaknessPhase1;
    public Transform[] spawnPointsLifePhase1;
    public Transform[] spawnPointsPlatformPhase2;
    public Transform[] spawnPointsWeaknessPhase2;
    public Transform[] spawnPointsLifePhase2;
    public Transform[] spawnPointsBrWallPhase2;
    public Transform[] spawnPointsWeaknessPhase3; //only weakness

    public Transform[] enemySpawnPointsPhase1;
    public Transform[] enemySpawnPointsPhase2;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    void OnEnable()
    {
        destroyedWeaknesses = 0;
        spawned = false;
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
        platformSpawned = false;
    }

    void Update()
    {
        Attack();

        if (phaseCount == 1 || phaseCount == 2)
        {
            fireCooldownTimer -= Time.deltaTime;
            if (fireCooldownTimer <= 0)
            {
                FireMissile();
                fireCooldownTimer = fireCooldown;
            }
        }
        /*if (PlatformTrigger.platformActivated && !platformSpawned)
        {
            SpawnObjects(spawnPointPlatformConst, platformPrefab);
            platformSpawned = true;
          
        }*/
        
    }

    private void FireMissile()
    {
        GameObject missileInstance = null;

        if (phaseCount == 1)
        {
            missileInstance = Instantiate(normalMissilePrefab, shootPoint.position, shootPoint.rotation);

            Fire fireComponent = missileInstance.GetComponent<Fire>();
           
            if (fireComponent != null)
            {
                Vector2 direction = (player.transform.position - shootPoint.position).normalized;
                fireComponent.FireShot(direction);
            }
        }
        else if (phaseCount == 2) 
        {
            missileInstance = Instantiate(homingMissilePrefab, shootPoint.position, shootPoint.rotation);

            HomingMissile homingMissile = missileInstance.GetComponent<HomingMissile>();
            if (homingMissile != null)
            {
                homingMissile.player = player;
            }
        }
    }

    public override void Attack()
    {
        switch (phaseCount)
        {
            case 0:
                Phase1();
                break;
            case 1:
                Phase2();
                break;
            case 2:
                Phase3();
                break;
            case 3:
                Die();
                break;
        }
    }

    private void Phase1()
    {
        if (!spawned)
        {
            SpawnObjects(spawnPointsPlatformPhase1, platformPrefab);
            SpawnObjects(spawnPointsWeaknessPhase1, weaknessPrefab);
            SpawnObjects(spawnPointsLifePhase1, lifePrefab);

            SpawnEnemies(new[] { "EnemyType1", "EnemyType2" }, enemySpawnPointsPhase1);
            spawned = true;
        }
    }

    private void Phase2()
    {
        if (!spawned)
        {
            DespawnEnemies();
            DespawnAllObjects();
            SpawnObjects(spawnPointsPlatformPhase2, platformPrefab);
            SpawnObjects(spawnPointsWeaknessPhase2, weaknessPrefab);
            SpawnObjects(spawnPointsBrWallPhase2, breakableWallPrefab);
            SpawnObjects(spawnPointsLifePhase2, lifePrefab);

            SpawnEnemies(new[] { "EnemyType1", "EnemyType2", "EnemyType3" }, enemySpawnPointsPhase2);
            destroyedWeaknesses = 0;
            spawned= true;
        }
    }

    private void Phase3()
    {
        if (!spawned)
        {
            destroyedWeaknesses = 0;
            DespawnEnemies();
            DespawnAllObjects();
            SpawnObjects(spawnPointsWeaknessPhase3, weaknessPrefab);
            SpawnThornObstacle();
            spawned = true;
        }
    }

    private void SpawnObjects(Transform[] spawnPoints, GameObject prefab)
    {
        foreach (var spawnPoint in spawnPoints)
        {
            GameObject spawnedThings = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            spawnedObjects.Add(spawnedThings);
        }
    }
    private void DespawnAllObjects()
    {
        foreach (var obj in spawnedObjects)
        {
            if (obj != null) Destroy(obj);
        }
        spawnedObjects.Clear();
    }
    private void SpawnEnemies(string[] enemyTypes, Transform[] phaseEnemySpawnPoints)
    {
        if (enemyTypes.Length < phaseEnemySpawnPoints.Length)
        {
            List<string> extendedEnemyTypes = new List<string>();

            //wiederholen der types
            while (extendedEnemyTypes.Count < phaseEnemySpawnPoints.Length)
            {
                extendedEnemyTypes.AddRange(enemyTypes);
            }

            enemyTypes = extendedEnemyTypes.ToArray();
        }

        for (int i = 0; i < phaseEnemySpawnPoints.Length; i++)
        {
            string enemyType = enemyTypes[i % enemyTypes.Length];
            GameObject enemyPrefab = Resources.Load<GameObject>($"Enemies/{enemyType}");

            if (enemyPrefab != null)
            {
                GameObject spawnedEnemy = Instantiate(enemyPrefab, phaseEnemySpawnPoints[i].position, Quaternion.identity);
                spawnedEnemies.Add(spawnedEnemy);
            }
        }
    }


    private void SpawnThornObstacle()
    {
        Vector3 position = transform.position;
        position.y += Random.Range(0, 1) == 0 ? 0 : 1;

        GameObject thornObstacle = Instantiate(thornObstaclePrefab, position, Quaternion.identity);
        spawnedObjects.Add(thornObstacle);

        Thorns movement = thornObstacle.GetComponent<Thorns>();
        if (movement != null)
        {
            movement.moveRange = thornRange; 
            movement.moveSpeed = thornSpeed;
        }
    }
    public void OnWeaknessDestroyed()
    {
        destroyedWeaknesses++;

        if (destroyedWeaknesses >= spawnPointsWeaknessPhase1.Length) 
        {
            StopAttack();
        }
        else if (destroyedWeaknesses >= spawnPointsWeaknessPhase1.Length + spawnPointsWeaknessPhase2.Length)
        {
            StopAttack();
        }
    }
    private void DespawnEnemies()
    {
        foreach (var enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy); 
            }
        }
        spawnedEnemies.Clear(); 
    }
    public void newPhase()
    {
        spawned = false;
    }
    public override void StopAttack()
    {
        newPhase();
        phaseCount++;
        if (phaseCount > 3) phaseCount = 3;
    }

    private void Die()
    {
        Debug.Log("boss defeated");
        Destroy(gameObject);
        DespawnAllObjects();
        bossActive = false;
    }
}
