using System.Threading;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class StationaryEnemy : EnemyBase
{
    [InlineEditor(InlineEditorModes.GUIOnly)]
    public StationaryEnemyStats eStats;
    protected PlayerHealth playerHealth;
    protected SpriteRenderer spriteRenderer;
    private bool isObstacle;

    public GameObject counterUIPrefab; 
    protected CounterUI counterUIInstance;
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

    protected Animator anim; //base klasse?
    protected float distance; //zwischen player und enemy
    public float timer;
    protected float intTimer; //store den initial timer

    private void Awake()
    {
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
            spriteRenderer.flipX = false;
        }
        else if (direction > 0)
        {
            spriteRenderer.flipX = true;
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
    private void OnTriggerEnter2D(Collider2D other) //weapon hitbox muss in den trigger
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("trigger");
            playerHealth = other.GetComponent<PlayerHealth>();
         
            playerHealth.TakeDamage(stats.damage);
            Debug.Log("taking damage: " +stats.damage);
        }
    }

    /*private void OnTriggerStay2D(Collider2D other)
    {
        //timer hinzufügen
        playerHealth.TakeDamage(stats.damage);
        Debug.Log("taking damage: " + stats.damage);
    }*/
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
