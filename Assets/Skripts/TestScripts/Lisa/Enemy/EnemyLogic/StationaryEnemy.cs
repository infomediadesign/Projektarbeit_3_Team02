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
    protected float counterTimer = 0.2f; // 2 frames
    protected float counterTimerRemaining;

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
        if (counterUIPrefab != null)
        {
            GameObject ui = Instantiate(counterUIPrefab);
            counterUIInstance = ui.GetComponent<CounterUI>();
            counterUIInstance.target = transform;
            counterUIInstance.offset = new Vector3(-10, 4, 0);
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
    /*private void UpdateCounterUI()
    {
        if (counterUIInstance != null && counterPossible)
        {
            float timeLeft = Mathf.Max(0, timer); 
            counterUIInstance.UpdateTimer(timeLeft, intTimer); 
        }
    }*/
    [Button]
    public override void Attack()
    {
        canAttack = true;
        timer = intTimer;
        anim.SetBool("Moving", false);
        anim.SetBool("Attacking", true);
        StartCounterWindow();

    }
    protected void StartCounterWindow()
    {
        counterPossible = true;
        counterTimerRemaining = counterTimer;

        if (counterUIInstance != null)
        {
            counterUIInstance.ResetTimer();
            counterUIInstance.Show();
        }

        InvokeRepeating(nameof(UpdateCounterWindow), 0, 0.1f);
    }
    protected void UpdateCounterWindow()
    {
        if (!counterPossible) return;

        counterTimerRemaining -= 0.1f;
        if (counterUIInstance != null)
        {
            counterUIInstance.UpdateTimer(counterTimerRemaining, counterTimer);
        }

        if (counterTimerRemaining <= 0)
        {
            EndCounterWindow();
        }
    }
    protected void EndCounterWindow()
    {
        counterPossible = false;
        CancelInvoke(nameof(UpdateCounterWindow));
        if (counterUIInstance != null)
        {
            counterUIInstance.Hide();
        }
    }


    public override void StopAttack()
    {
        cooling = false;
        canAttack = false;
        anim.SetBool("Attacking", false);
        EndCounterWindow();
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
            Debug.Log("taking damage: " +stats.damage);
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

    /*public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, eStats.attackRad);
        Gizmos.color = Color.white;
    }*/
}
