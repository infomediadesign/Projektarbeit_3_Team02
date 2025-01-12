using UnityEngine;

public class FlyingEnemy : EnemyBase
{
    public FlyingEnemyStats fStats;
    protected PlayerHealth playerHealth;
    protected SpriteRenderer spriteRenderer;
    protected Animator anim;
    public Transform player;

    public Transform[] patrolPoints;
    private int targetPoint;

    public GameObject firePrefab;
    public Transform shootPoint;
    private float nextShootTime = 0f;

    private void Awake()
    {
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
        Patrol();
        CheckForPlayerAndShoot();
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
        Vector2 direction = patrolPoints[targetPoint].position - transform.position;

        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[targetPoint].position, fStats.flySpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, patrolPoints[targetPoint].position) < 0.1f)
        {
            increaseTarget();
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("trigger");
            playerHealth = other.GetComponent<PlayerHealth>();

            playerHealth.TakeDamage(stats.damage);
            Debug.Log("taking damage");

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
