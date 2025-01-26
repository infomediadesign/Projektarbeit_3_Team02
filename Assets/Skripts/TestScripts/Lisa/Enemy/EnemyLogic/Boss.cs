using UnityEngine;

public class Boss : EnemyBase
{
    private float phaseCount = 0;
    public GameObject firePrefab;
    public Transform shootPoint;
    public float fireCooldown = 3f;  

    private float fireCooldownTimer = 0f; 
    void Awake()
    {

    }

    void Update()
    {
        fireCooldownTimer -= Time.deltaTime;

        if (fireCooldownTimer <= 0)
        {
            FireMissile();
            fireCooldownTimer = fireCooldown;
        }
    }
    private void FireMissile()
    {

        GameObject missileInstance = Instantiate(firePrefab, shootPoint.position, shootPoint.rotation);

        HomingMissile missile = missileInstance.GetComponent<HomingMissile>();
        if (missile != null)
        {
            missile.target = GameObject.FindGameObjectWithTag("Player").transform; 
        }
    }
    public override void Attack()
    {
        if(phaseCount == 0) 
        {
            //SpawnEnemies();
            //SpawnPlatform();
            //SpawnCollectibles();
            //SpawnWeakness();
        }
        else if(phaseCount == 1)
        {
            //SpawnEnemies();
            //SpawnCollectibles();
            //SpawnWeakness();
            //SpawnPlatform();
            //FireProjectiles();
        }
        else if( phaseCount == 2)
        {
            //SpawnCollectibles();
            //SpawnWeakness();
            //FireTargetProjectiles();
        }
        else if(phaseCount == 3)
        {
            Die();
        }
    }

    public override void StopAttack()
    {
        phaseCount++;
    }
}
