using UnityEngine;
using UnityEngine.InputSystem;

public class StateManager : MonoBehaviour
{
    public InputSystem_Actions playerControls;
    public InputAction walk { get; private set; }
    public InputAction jump { get; private set; }
    public InputAction block { get; private set; }
    public InputAction roll { get; private set; }
    public InputAction counter { get; private set; }
    public InputAction airCounter { get; private set; }


    //UNTEN sollte größtenteil in Scriptable Object
    private float xInput;
    private bool isMovementPressed;
    private bool isJumpPressed;


    public Rigidbody2D rb;
    public Transform position;
    public float walkingSpeed = 5;
    public float jumpForce = 10;
    public CapsuleCollider2D capCol;

    public BaseState currentState;
    //public StateFactory states;

    public bool isGrounded { get; private set; }
    public Transform groundCheckPos;
    public float groundCheckRad = 0.2f;
    public LayerMask groundLayer;
    public float jumpMultiplier = 0.5f;
    public bool left;

    public bool isEnemy { get; private set; }
    public Transform enemyCheckPos;
    public float enemyCheckRad = 0.5f;
    public LayerMask enemyLayer;


    public float rollDistance = 3;
    public float rollSpeed = 3;
    private bool facingRight = true;

    // OBEN sollte größtenteils in Scriptable Object
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
        playerControls = new InputSystem_Actions();
       // states = new StateFactory(this);
        currentState = idleState;
      
    }

    private void OnEnable() //kann man auch mit ganzer map machen
    {
        walk = playerControls.Test.Walk;
        walk.Enable();
        jump = playerControls.Test.Jump;
        jump.Enable();
        roll = playerControls.Test.Roll;
        roll.Enable();
        counter = playerControls.Test.Counter;
        counter.Enable();
        block = playerControls.Test.Block;
        block.Enable();
        airCounter = playerControls.Test.AirCounter;
        airCounter.Enable();
    }
    void OnDisable()
    {
        walk.Disable();
        jump.Disable();
        block.Disable();
        roll.Disable();
        counter.Disable();
        airCounter.Disable();
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
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRad, groundLayer);
        isEnemy = Physics2D.OverlapCircle(enemyCheckPos.position, enemyCheckRad, enemyLayer);

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




}
