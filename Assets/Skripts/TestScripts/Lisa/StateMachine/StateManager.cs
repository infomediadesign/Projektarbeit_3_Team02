using UnityEngine;
using UnityEngine.InputSystem;

public class StateManager : MonoBehaviour
{
    [HideInInspector] public PlayerCombat playerCombat;
    public GameObject player;
    public EnemyBase targetEnemy;
    [HideInInspector] public EnemyBase currentEnemy;

    public InputSystem_Actions inputActions;
    public InputSystem_Actions.TestActions playerControls;
    //public PlayerInputConfig playerInput;
    public PlayerStats playerStats;

   /* private float xInput;
    private bool isMovementPressed;
    private bool isJumpPressed;*/

    public Rigidbody2D rb;
    public Transform position;
    public CapsuleCollider2D capCol;

    public BaseState currentState;
    //public StateFactory states;

    public bool isGrounded { get; private set; }
    public Transform groundCheckPos;
    public LayerMask groundLayer;

    public bool isEnemy { get; private set; }
    public bool shielded { get; private set; }
    public Transform enemyCheckPos;
    public LayerMask enemyLayer;

    private bool facingRight = true;

    public Walk walkState = new Walk();
    public Jump jumpState = new Jump();
    public JumpBlock jumpBlockState = new JumpBlock();
    public Roll rollState = new Roll();
    public Counter counterState = new Counter();
    public AirCounter airCounterState = new AirCounter();
    public Block blockState = new Block();
    public Idle idleState = new Idle();
    public Falling fallingState = new Falling();
   // public Grounded groundedState = new Grounded();


    void Awake()
    {
        inputActions = new InputSystem_Actions();
        playerControls = inputActions.Test;
       // states = new StateFactory(this);
        currentState = idleState;
      
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capCol = GetComponent<CapsuleCollider2D>();
        playerCombat = GetComponent<PlayerCombat>();
     
        //currentState = idleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, playerStats.groundCheckRad, groundLayer);
        isEnemy = Physics2D.OverlapCircle(enemyCheckPos.position, playerStats.enemyCheckRad, enemyLayer);
        if(currentState == blockState)
        {
            shielded = true;
        }
        else
        {
            shielded = false;
        }

        currentState.UpdateState(this);
    }

    public void TransitionState(BaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        state.EnterState(this);
       
    }
   

    public void SetFacingDirection(bool isFacingRight)
    {
        if (facingRight != isFacingRight)
        {
            facingRight = isFacingRight;

            Vector3 localScale = transform.localScale;
            localScale.x = isFacingRight ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
    }
    void OnDisable()
    {
        playerControls.Disable();
    }
    public bool CheckForEnemy() //hier muss für normalen counter noch ein radius eingefügt werden
    {
        Collider2D enemyCollider = Physics2D.OverlapCircle(enemyCheckPos.position, playerStats.enemyCheckRad, enemyLayer);
        if (enemyCollider != null)
        {
            currentEnemy = enemyCollider.GetComponent<EnemyBase>();
            return currentEnemy != null; 
        }

        currentEnemy = null;
        return false;
    }


}
