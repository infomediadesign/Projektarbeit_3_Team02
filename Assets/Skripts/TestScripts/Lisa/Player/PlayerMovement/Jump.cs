using UnityEngine;

public class Jump : BaseState
{
    public Jump(StateManager currentContext, StateFactory factory)
    : base(currentContext, factory) 
    {
        InitializeSubState();
        isRootState = true;
    }
    private float xInput;
    private bool jumpReleased;
    
    override public void EnterState()
    {
        context.playerControls.Jump.canceled += OnJumpCanceled;       
    }
    override public void UpdateState()
    {
        CheckSwitchStates();
        xInput = context.playerControls.Walk.ReadValue<float>();
        context.rb.linearVelocity = new Vector2(xInput * context.playerStats.walkingSpeed, context.rb.linearVelocity.y);

        if (context.isGrounded)
        {
            context.rb.linearVelocity = new Vector2(context.rb.linearVelocity.x, context.playerStats.jumpForce);
        }
        else if (jumpReleased && context.rb.linearVelocity.y > 0)
        {
            context.rb.linearVelocity = new Vector2(context.rb.linearVelocity.x, context.rb.linearVelocity.y * context.playerStats.jumpMultiplier);
            jumpReleased = false;
        }

        if(context.rb.linearVelocityX < 0)
        {
            context.SetFacingDirection(false);
        }
        else
        {
            context.SetFacingDirection(true);
        }
    }

    public override void CheckSwitchStates()
    {
        if (context.isGrounded)
        {
            //state.TransitionState(state.walkState);
            SwitchState(factory.Ground());
        }
    }

    public override void InitializeSubState()
    {
        if (context.rb.linearVelocityY < 0)
        {
            //context.TransitionState(context.fallingState);
            SetSubState(factory.Fall());
        }
        else if (context.playerControls.AirCounter.triggered && !context.isGrounded && context.isEnemy)
        {
            //state.TransitionState(state.airCounterState);
            SetSubState(factory.AirCountering());
        }
        else if (context.playerControls.Block.triggered)
        {
            //state.TransitionState(state.jumpBlockState);
            SetSubState(factory.Blocking());
        }
        else if (context.playerControls.Roll.triggered && context.rb.linearVelocityX != 0)
        {
            //state.TransitionState(state.rollState);
            SetSubState(factory.Rolling());
        }
        else if (context.playerControls.Walk.triggered)
        {
            SetSubState(factory.Walking());
        }
        else
        {
            SetSubState(factory.Idleing());
        }
    }

    override public void ExitState()
    {
        context.playerControls.Jump.canceled -= OnJumpCanceled;
    }
    private void OnJumpCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        jumpReleased = true;
    }
}
