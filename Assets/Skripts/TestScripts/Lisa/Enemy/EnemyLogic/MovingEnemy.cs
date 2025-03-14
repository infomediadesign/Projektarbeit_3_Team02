
using UnityEngine;
using UnityEngine.UIElements;

public class MovingEnemy : StationaryEnemy
{
    public bool isPatroling;
    private bool playerInRange;
    public Transform groundCheck; 
    private float groundCheckDistance = 1f;
    public LayerMask groundLayer;
    public CapsuleCollider2D enemyHitbox;
    private bool isWallAhead;

    public MovingEnemyStats mStats;

    public Transform raycast;
    public LayerMask raycastMask;
    public float raycastLength;
    public float moveSpeed; //theoretisch ins SO falls alle gleich

    private RaycastHit2D hit;
    private GameObject target;

    public Transform[] patrolPoints;
    public int targetPoint;
    private bool playerInSoundRange;


    public void Awake()
    {
        playerInSoundRange = false;
        intTimer = timer;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        counterPossible = false;
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
        if (Vector2.Distance(player.transform.position, transform.position) <= 5f)
        {
            playerInSoundRange = true;
        }
        else
        {
            playerInSoundRange = false;
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
        float direction = player.position.x - transform.position.x;
        Rotate();
        if (isPatroling && !playerInRange )
        {
            anim.SetBool("Moving", true);
            Patrol();
            if (playerInSoundRange && !isDying)
            {
                PlaySound("WalkingEnemyWalk");
            }
        }
        else if(playerInRange)
        {
            if (direction < 0)
            {
                hit = Physics2D.Raycast(raycast.position, Vector2.left, raycastLength, raycastMask);
            }
            else
            {
                hit = Physics2D.Raycast(raycast.position, Vector2.right, raycastLength, raycastMask);
            }
            
        }
        if(hit.collider != null)
        {
            EnemyLogic();
        }
        else if(hit.collider == null)
        {
            playerInRange = false;
        }
        if (!playerInRange && !isPatroling)
        {
            anim.SetBool("Moving", false);
            StopAttack();
            if (playerInSoundRange && !isDying)
            {
                PlaySound("WalkingEnemyIdle");
            }
        }
        else if(!playerInRange && isPatroling)
        {
            StopAttack();
        }
        if (damageCooldownTimer > 0f)
        {
            damageCooldownTimer -= Time.deltaTime;
        }
    }

    public void Patrol()
    {
        if (!isDying)
        {
            Vector2 direction = patrolPoints[targetPoint].position - transform.position;

            if (direction.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = true;
            }

            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[targetPoint].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[targetPoint].position) < 0.1f)
            {
                increaseTarget();
            }
        }
    }
    private bool IsGroundAhead()
    {
        RaycastHit2D groundHit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        return groundHit.collider != null; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {           
            target = other.gameObject;
            playerInRange = true;
            playerHealth = other.GetComponent<PlayerHealth>();

            if (other.IsTouching(enemyHitbox) && !isDying || !isDying && other.IsTouching(attackCollider) && attackCollision)
            {
                if (other.transform.position.y < transform.position.y)
                {
                    playerHealth.TakeDamage(stats.damage);
                    Debug.Log("taking damage: " + stats.damage);
                }

            }
           
        }
        if (other.CompareTag("Ground") && other.IsTouching(enemyHitbox))
        {
            isWallAhead = true;
        }
     }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float verticalDifference = Mathf.Abs(transform.position.y - other.transform.position.y);
            if (verticalDifference <= mStats.maxVerticalRange)
            {
                playerInRange = true;
            }
            else
            {
                playerInRange = false;
            }

             if (!isDying && other.IsTouching(attackCollider))
            {
                if (attackCollision)
                {
                    playerHealth.TakeDamage(stats.damage);
                    Debug.Log("taking damage: " + stats.damage);
                    attackCollision = false;
                }
            }
        }
       
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            damageCooldownTimer = 0;
        }
        if (other.CompareTag("Ground"))
        {
            isWallAhead = false;
        }
    }

    private void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        float verticalDifference = Mathf.Abs(transform.position.y - player.transform.position.y);
        if (!IsGroundAhead())
        {
            playerInRange = false;
            isPatroling = true;
            return;
        }
        if (distance > mStats.attackRad)
        {
            Move();
            StopAttack();
        }
        if (mStats.attackRad >= distance && verticalDifference <= mStats.maxVerticalRange && !cooling)
        {
            Attack();
            if (playerInSoundRange)
            {
                SoundManager.Instance.StopEnemySound2D();
                PlaySound("WalkingEnemyAttack");
            }
        }
        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attacking", false);
        }
    }

    private void Move()
    {
        if (!isDying && !isWallAhead)
        {
            if (playerInSoundRange)
            {
                PlaySound("WalkingEnemyWalk");
            }
            anim.SetBool("Moving", true);
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("enemyAttack"))
            {
                Vector2 targetPostition = new Vector2(target.transform.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPostition, moveSpeed * Time.deltaTime);
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
}
