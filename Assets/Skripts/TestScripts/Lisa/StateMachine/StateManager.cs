using UnityEngine;
using UnityEngine.InputSystem;

public class StateManager : MonoBehaviour
{
    //UNTEN sollte größtenteil in Scriptable Object

    public Rigidbody2D rb;
    public Transform position;
    public float walkingSpeed = 5;
    public float jumpForce = 10;

    public BaseState currentState;
    public Walk walkState = new Walk();
    public Jump jumpState = new Jump();
    public JumpBlock jumpBlockState = new JumpBlock();
    public Roll rollState = new Roll();
    public Counter counterState = new Counter();
    public AirCounter airCounterState = new AirCounter();
    public Block blockState = new Block();
    public Idle idleState = new Idle();

    public bool isGrounded { get; private set; }
    public Transform groundCheckPos;
    public float groundCheckRad = 0.2f;
    public LayerMask groundLayer;
    public float jumpMultiplier = 0.5f;

    public bool isEnemy { get; private set; }
    public Transform enemyCheckPos;
    public float enemyCheckRad = 0.5f;
    public LayerMask enemyLayer;


    public float rollDistance = 3;
    public float rollSpeed = 3;

    // OBEN sollte größtenteils in Scriptable Object

    public InputActionAsset inputActions;

    private InputAction walkAction;
    private InputAction jumpAction;
    private InputAction blockAction;

    public InputAction GetWalkAction() => walkAction;
    public InputAction GetJumpAction() => jumpAction;
    public InputAction GetBlockAction() => blockAction;
    void Awake()
    {
        walkAction = inputActions.FindAction("Walk");
        jumpAction = inputActions.FindAction("Jump");
        blockAction = inputActions.FindAction("Block");

        walkAction.Enable();
        jumpAction.Enable();
        blockAction.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = idleState;
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
        state.ExitState(this);
        currentState = state;
        state.EnterState(this);

    }
    void OnDisable()
    {
        walkAction.Disable();
        jumpAction.Disable();
        blockAction.Disable();
    }

}
