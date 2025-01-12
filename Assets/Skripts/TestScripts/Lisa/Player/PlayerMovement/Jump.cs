using UnityEngine;

public class Jump : BaseState
{
    public Jump(StateManager currentContext, StateFactory factory)
    : base(currentContext, factory) { }

    private float xInput;
    private bool hasJumped = false;
    private bool hasReleased = false;

    override public void EnterState()
    {
        hasReleased = false;
        hasJumped = false;
    }

    override public void UpdateState()
    {
        
        xInput = context.playerControls.Walk.ReadValue<float>();
        context.rb.linearVelocity = new Vector2(xInput * context.playerStats.walkingSpeed, context.rb.linearVelocity.y);

        if (context.isGrounded)
        {
            if (!hasJumped)
            {
                context.rb.linearVelocity = new Vector2(context.rb.linearVelocity.x, context.playerStats.jumpForce);
                hasJumped = true;
            }
            
        }
        else if (context.jumpReleased && context.rb.linearVelocity.y > 0 )
        {
            if (!hasReleased)
            {
                context.rb.linearVelocity = new Vector2(context.rb.linearVelocity.x, context.rb.linearVelocity.y * context.playerStats.jumpMultiplier);
                context.jumpReleased = false;
                hasReleased = true;
            }

        }

        if (context.rb.linearVelocity.x < 0)
        {
            context.SetFacingDirection(false);
        }
        if (context.rb.linearVelocity.x > 0)
        {
            context.SetFacingDirection(true);
        }
        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (context.rb.linearVelocity.y < 0)
        {
            SwitchState(factory.Fall());
        }
        else if (context.playerControls.AirCounter.triggered && !context.isGrounded && context.isEnemy)
        {
            SwitchState(factory.AirCountering());
        }
        else if (context.playerControls.Block.triggered)
        {
            SwitchState(factory.Blocking());
        }
    }

    public override void InitializeSubState()
    {
    }

    override public void ExitState()
    {
        
    }

   
}

