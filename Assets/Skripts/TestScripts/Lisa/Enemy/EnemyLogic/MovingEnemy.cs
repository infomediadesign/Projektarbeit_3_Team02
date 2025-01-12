using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingEnemy : StationaryEnemy
{
    public bool isPatroling;
    private bool playerInRange;

    public MovingEnemyStats mStats;

    public Transform raycast;
    public LayerMask raycastMask;
    public float raycastLength;
    public float moveSpeed; //theoretisch ins SO falls alle gleich

    private RaycastHit2D hit;
    private GameObject target;

    public Transform[] patrolPoints;
    public int targetPoint;


    private void Awake()
    {
        intTimer = timer;
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
        float direction = player.position.x - transform.position.x;
        Rotate();
        if (isPatroling && !playerInRange )
        {
            anim.SetBool("Moving", true);
            Patrol();
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
        }
        else if(!playerInRange && isPatroling)
        {
            StopAttack();
        }
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

        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[targetPoint].position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, patrolPoints[targetPoint].position) < 0.1f)
        {
            increaseTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.gameObject;
            playerInRange = true;
      

        }
    }

    private void EnemyLogic()
    {
        Debug.Log(distance);
        distance = Vector2.Distance(transform.position, player.transform.position);
        float verticalDifference = Mathf.Abs(transform.position.y - player.transform.position.y);
        if (distance > mStats.attackRad)
        {
            Move();
            StopAttack();
        }
        if (mStats.attackRad >= distance && verticalDifference <= mStats.maxVerticalRange && !cooling)
        {
            Attack();
        }
        if (cooling)
        {
            anim.SetBool("Attacking", false);
        }
    }

    private void Move()
    {
        anim.SetBool("Moving", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("enemyAttack"))
        {
            Vector2 targetPostition = new Vector2(target.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPostition, moveSpeed * Time.deltaTime);
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
