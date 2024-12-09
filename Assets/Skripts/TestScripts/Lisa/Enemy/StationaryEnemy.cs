using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class StationaryEnemy : EnemyBase
{
    public StationaryEnemyStats eStats;
    protected PlayerHealth playerHealth;
    protected SpriteRenderer spriteRenderer;
    private bool isObstacle;

    public Transform player;
    protected bool canAttack = true;
    protected bool cooling;

    protected Animator anim; //base klasse?
    protected float distance; //zwischen player und enemy
    public float timer;
    protected float intTimer; //store den initial timer

    private void Awake()
    {
        intTimer = timer;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (!isObstacle)
        {
            Rotate();
            CheckDistance();
        }
      
    }

    public override void Attack()
    {
        canAttack = true;
        timer = intTimer;
        anim.SetBool("Moving", false);
        anim.SetBool("Attacking", true);

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

        if (direction < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction > 0)
        {
            spriteRenderer.flipX = false;
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
            Cooldown();
            anim.SetBool("Attacking", false);
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
