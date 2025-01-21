using UnityEngine;

public class Boss : EnemyBase
{
    private float phaseCount = 0;
    public GameObject firePrefab;
    public Transform shootPoint;
    public float fireCooldown = 3f;  

    private float fireCooldownTimer = 0f; 
    private Transform playerTarget;
    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTarget = player.transform;
        }
    }

    void Update()
    {
        fireCooldownTimer -= Time.deltaTime;

        if (fireCooldownTimer <= 0 && playerTarget != null)
        {
            FireFollowingProjectile();
            fireCooldownTimer = fireCooldown;
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
    void FireFollowingProjectile()
    {

        GameObject fireInstance = Instantiate(firePrefab, shootPoint.position, Quaternion.identity);

        Fire fire = fireInstance.GetComponent<Fire>();
        if (fire != null)
        {
            fire.FireFollowing(playerTarget); 
        }
        
    }
    public override void StopAttack()
    {
        phaseCount++;
    }
}
