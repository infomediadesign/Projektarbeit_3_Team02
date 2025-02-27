
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class StateManager : MonoBehaviour
{
    [HideInInspector] public PlayerCombat playerCombat;
    public GameObject player;
    [HideInInspector] public EnemyBase targetEnemy;
    [HideInInspector] public EnemyBase currentEnemy;
    [HideInInspector] public CapsuleCollider2D mainCollider;
    public CapsuleCollider2D rollTrigger;
    public CapsuleCollider2D jumpCollider;
    [HideInInspector] public CounterZone zone;
    [HideInInspector] public Animator animator;
    private SpriteRenderer sRenderer;
    

    public InputSystem_Actions inputActions;
    public InputSystem_Actions.TestActions playerControls;
    public PlayerStats playerStats;

    [HideInInspector] public Rigidbody2D rb;
    public Transform position;
    [HideInInspector] public CapsuleCollider2D capCol;

    public BaseState currentState;
    public BaseState currentSubState;
    public StateFactory states;

    public bool isGrounded { get; private set; }
    public Transform groundCheckPos;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public LayerMask groundLayer;
    public LayerMask groundEnemyLayer;


    public bool isEnemy { get; private set; }
    public bool isGroundEnemy { get; private set; }
    public bool shielded;
    public bool rolling { get; private set; }
    public bool countering { get; private set; }
    public bool jumpReleased;
    public Transform enemyCheckPos;
    public LayerMask enemyLayer;

    private bool facingRight = false;
    [HideInInspector] public bool counterPossible = true;
    [HideInInspector] public bool counterWindow;
    [HideInInspector] public bool damageAnim;
    [HideInInspector] public bool isObstacle = false;



    void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        inputActions = new InputSystem_Actions();
        playerControls = inputActions.Test;
        states = new StateFactory(this);
        mainCollider = GetComponent<CapsuleCollider2D>();
        zone = GetComponentInChildren<CounterZone>();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capCol = GetComponent<CapsuleCollider2D>();
        playerCombat = GetComponent<PlayerCombat>();
        currentState = states.Ground();
        currentState.EnterState();
       

    }


    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Jump.canceled += OnJumpCanceled;
        playerControls.Jump.started += OnJumpStarted;
    }



    void Update()
    {
       // isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, playerStats.groundCheckRad, groundLayer);

        isEnemy = Physics2D.OverlapCircle(enemyCheckPos.position, playerStats.enemyCheckRad, enemyLayer);
        isGroundEnemy = Physics2D.OverlapCircle(enemyCheckPos.position, playerStats.enemyCheckRad, groundEnemyLayer);

       /* if (currentSubState == states.Blocking())
        {
            shielded = true;
            Debug.Log("shielded is true");
        }
        else
        {
            shielded = false;
        }
        if (currentSubState == states.Rolling()) //verwende ich das noch?
        {
            rolling = true;
        }
        else
        {
            rolling = false;
        }
        if (currentSubState == states.Countering())
        {
            countering = true;
        }
        else
        {
            countering = false;
        }*/

        currentState.UpdateStates();
    }

    public void SetFacingDirection(bool isFacingRight)
    {
        if (facingRight != isFacingRight)
        {
            facingRight = isFacingRight;

            Vector3 localScale = transform.localScale;
            localScale.x = isFacingRight ? -Mathf.Abs(localScale.x) : Mathf.Abs(localScale.x); //habs umgedreht lul
            transform.localScale = localScale;
        }
    }
    void OnDisable()
    {
        playerControls.Disable();
        playerControls.Jump.canceled -= OnJumpCanceled;
        playerControls.Jump.started -= OnJumpStarted;
    }
    private void OnJumpCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

        jumpReleased = true;

    }
    private void OnJumpStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

        jumpReleased = false;

    }
    public bool CheckForEnemy() //radius für berühren und counter (extra)
    {
        Collider2D enemyCollider = Physics2D.OverlapCircle(enemyCheckPos.position, playerStats.enemyCheckRad, enemyLayer);
        if (enemyCollider != null)
        {
            currentEnemy = enemyCollider.GetComponent<EnemyBase>();
            return currentEnemy != null;
        }
        else
        {
            enemyCollider = Physics2D.OverlapCircle(enemyCheckPos.position, playerStats.enemyCheckRad, groundEnemyLayer);

            if (enemyCollider != null)
            {
                currentEnemy = enemyCollider.GetComponent<EnemyBase>();
                return currentEnemy != null;
            }
        }

        currentEnemy = null;
        return false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
    
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("BreakableObstacle"))
        {
            isObstacle = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("BreakableObstacle"))
        {
            isObstacle = false;
        }

    }
    public void setDamageTaken()
    {
        damageAnim = false;
    }
    public void setCounterPossible()
    {
        counterPossible = true;
    }
    public void setCounterNotPossible()
    {
        counterPossible = false;
    }
    public void setCounterWindow()
    {
        counterWindow = true;
    }
    public void setNoCounterWindow()
    {
        counterWindow = false;
    }
    public IEnumerator FlashGreen()
    {
        sRenderer.color = Color.green;
        yield return new WaitForSeconds(0.5f);
        sRenderer.color = Color.white;
    }
    public IEnumerator FreezeAnimation(float duration)
    {
        if (animator != null)
        {
            animator.speed = 0;
            yield return new WaitForSecondsRealtime(duration);
            animator.speed = 1;
        }
    }

}
