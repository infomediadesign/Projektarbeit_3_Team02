using UnityEngine;

public class FlyingEnemy : EnemyBase
{
    public FlyingEnemyStats fStats;
    protected PlayerHealth playerHealth;
    protected SpriteRenderer spriteRenderer;
    protected Animator anim;
    public Transform player;
    public CapsuleCollider2D fEnemyHitbox;

    public Transform[] patrolPoints;
    private int targetPoint;

    public GameObject firePrefab;
    public Transform shootPoint;
    private float nextShootTime = 0f;

    private float damageCooldown = 1.5f; // kann man in base machen
    private float damageCooldownTimer = 0f;

    public bool patroling;
    public bool shootingEnemy = true;
    private bool playerInSoundRange;

    private void Awake()
    {
        playerInSoundRange = false;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;

            }
        }
       
    }

    void Update()
    {
        if(Vector2.Distance(player.transform.position, transform.position) <= 5f)
        {
            playerInSoundRange = true;
        }
        else
        {
            playerInSoundRange = false;
        }
        if (patroling)
        {
            Patrol();
           
        }
        if (shootingEnemy)
        {
            CheckForPlayerAndShoot();
        }
   
        if (isDying)
        {
            anim.SetBool("Death", true);
            if (playerInSoundRange)
            {
                SoundManager.Instance.StopEnemySound2D();
                PlaySound("EnemyDeath");
            }
        }
        else
        {
            anim.SetBool("Death", false);
        }
        if (damageCooldownTimer > 0f)
        {
            damageCooldownTimer -= Time.deltaTime;
        }
        if (playerInSoundRange && !isDying)
        {
            PlaySound("FlyingEnemyIdle");
        }
    }

    public override void Attack()
    {
        Vector2 direction = (player.transform.position - shootPoint.position).normalized;

        GameObject projectile = Instantiate(firePrefab, shootPoint.position, Quaternion.identity);
        projectile.GetComponent<Fire>().FireShot(direction);
    }

    public override void StopAttack()
    {
        
    }

    public void Patrol()
    {
        if (!isDying && patroling)
        {
            Vector2 direction = patrolPoints[targetPoint].position - transform.position;

            if (direction.x > 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (direction.x < 0)
            {
                spriteRenderer.flipX = false;
            }

            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[targetPoint].position, fStats.flySpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[targetPoint].position) < 0.1f)
            {
                increaseTarget();
            }
        }
    }
    void increaseTarget()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDying)
        {
            Debug.Log("trigger");
            playerHealth = other.GetComponent<PlayerHealth>();
            if (other.IsTouching(fEnemyHitbox))
            {
                if (damageCooldownTimer <= 0f)
                {
                    Debug.Log("trigger dmg");
                    playerHealth.TakeDamage(stats.damage);
                    Debug.Log("taking damage: " + stats.damage);
                    damageCooldownTimer = damageCooldown;
                }
            }

        }
    }*/


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            damageCooldownTimer = 0;
        }

    }
    private void CheckForPlayerAndShoot()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= fStats.shootingRange && Time.time >= nextShootTime)
        {
            Attack();
            nextShootTime = Time.time + fStats.shootCooldown;
        }
    }

}
