using UnityEngine;

public class StateManager : MonoBehaviour
{
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

    public bool isGrounded;
    public Transform groundCheckPos;
    public float groundCheckRad = 0.2f;
    public LayerMask groundLayer;
    public float jumpMultiplier = 0.5f;

    public float rollDistance = 3;
    public float rollSpeed = 3;

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

        currentState.UpdateState(this);
    }

    public void TransitionState(BaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}