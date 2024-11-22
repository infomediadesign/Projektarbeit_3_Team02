using UnityEngine;
using UnityEngine.InputSystem;

public class StateManager : MonoBehaviour
{
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
     
        //currentState = idleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, playerStats.groundCheckRad, groundLayer);
        isEnemy = Physics2D.OverlapCircle(enemyCheckPos.position, playerStats.enemyCheckRad, enemyLayer);

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


}
