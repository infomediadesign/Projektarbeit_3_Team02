using System.Threading;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class StationaryEnemy : EnemyBase
{
    [InlineEditor(InlineEditorModes.GUIOnly)]
    public StationaryEnemyStats eStats;
    protected PlayerHealth playerHealth;
    protected SpriteRenderer spriteRenderer;
    public CapsuleCollider2D sEnemyHitbox;
    private bool statEnemy;

    public GameObject counterUIPrefab; 
    protected float counterTimer = 0.6f; // frame 1-6
    protected float counterPossibleTimer = 0.4f; // frame 7 bis 10
    protected float counterAfterTimer = 0.5f; // frame 11 bis 15
    protected float counterPossibleTimerRemaining;
    protected float counterTimerRemaining;
    protected float counterAfterTimerRemaining;
    protected bool afterTimer = false;

    public Transform player;
    protected bool canAttack = true;
    protected bool cooling;
    protected float damageCooldown = 1.5f; // wann player schaden bekommen kann
    protected float damageCooldownTimer = 0f;

    protected Animator anim; //base klasse?
    protected float distance; //zwischen player und enemy
    public float timer;
    protected float intTimer; //store den initial timer
    private Vector3 originalOffset;
    private bool playerInSoundRange;
    private void Awake()
    {
        playerInSoundRange = false;
        statEnemy = true;
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
        originalOffset = spriteRenderer.transform.localPosition;
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
            Rotate();
            CheckDistance();
            if (isDying)
            {
                anim.SetBool("Death", true);
            if (playerInSoundRange)
            {
                PlaySound("EnemyDeath");
            }
            }
            if (damageCooldownTimer > 0f)
            {
                damageCooldownTimer -= Time.deltaTime;
            }
        if (playerInSoundRange)
        {
            PlaySound("StaticEnemyIdle");
        }
      
    }
    public override void Attack()
    {
        canAttack = true;
        timer = intTimer;
        anim.SetBool("Moving", false);
        anim.SetBool("Attacking", true);
        if (playerInSoundRange)
        {
            PlaySound("StaticEnemyAttack");
        }
      

    }  
    public override void StopAttack()
    {
        cooling = false;
        canAttack = false;
        anim.SetBool("Attacking", false);
    }
    protected void Rotate() // das einfach in base enemy packen
    {
        float direction = player.position.x - transform.position.x;
        float tolerance = 0.5f;
        if (statEnemy)
        {
            bool shouldFlipX = spriteRenderer.flipX; 

            if (direction < -tolerance)
            {
                shouldFlipX = false;
                spriteRenderer.transform.localPosition = new Vector3(originalOffset.x, spriteRenderer.transform.localPosition.y, spriteRenderer.transform.localPosition.z);
            }
            else if (direction > tolerance)
            {
                shouldFlipX = true;
                spriteRenderer.transform.localPosition = new Vector3(originalOffset.x + 0.45f, spriteRenderer.transform.localPosition.y, spriteRenderer.transform.localPosition.z);
            }

            if (spriteRenderer.flipX != shouldFlipX)
            {
                spriteRenderer.flipX = shouldFlipX;
            }
        }
        else
        {
            if (direction < 0)
            {
                spriteRenderer.flipX = false;

            }
            else if (direction > 0)
            {
                spriteRenderer.flipX = true;
            }
            else if(direction == 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
    private void CheckDistance()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        float verticalDifference = Mathf.Abs(transform.position.y - player.transform.position.y);
        if (eStats.attackRad >= distance && verticalDifference <= eStats.maxVerticalRange && !cooling)
        {
            Attack();
            anim.SetBool("Moving", false);
        }
        if (cooling)
        {
            anim.SetBool("Attacking", false);
            Cooldown(); 
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && !isDying)
        {
            playerHealth = other.GetComponent<PlayerHealth>();

            if (other.IsTouching(sEnemyHitbox))
            {
                playerHealth.TakeDamage(stats.damage);
                Debug.Log("taking damage: " + stats.damage);
            }

        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDying && other.IsTouching(sEnemyHitbox))
        {
            if (damageCooldownTimer <= 0f)
            {

                playerHealth.TakeDamage(stats.damage);
                Debug.Log("taking damage: " + stats.damage);

                damageCooldownTimer = damageCooldown;


            }
        }
        else if (other.CompareTag("Player") && !isDying && other.IsTouching(attackCollider))
        {
            if (attackCollision)
            {
                playerHealth.TakeDamage(stats.damage);
                Debug.Log("taking damage: " + stats.damage);
                attackCollision = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            damageCooldownTimer = 0;
        }
    }
    protected void SetCounterPossible()
    {
        counterPossible = true;
    }
    protected void SetCounterNotPossible()
    {
        counterPossible = false;
    }
    public void TriggerCooling()
    {
        cooling = true;
    }
    protected void Cooldown()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 &&  cooling && canAttack)
        {
            cooling = false;
            timer = intTimer;
        }
    }
}
