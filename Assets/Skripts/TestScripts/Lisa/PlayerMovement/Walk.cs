using UnityEngine;

public class Walk : BaseState
{

    private float xInput;

    override public void EnterState(StateManager state)
    {
        Debug.Log("entering walking state...");
   
    }


    override public void UpdateState(StateManager state)
    {
        
        xInput = state.walk.ReadValue<float>();
        state.rb.linearVelocity = new Vector2(xInput * state.walkingSpeed, state.rb.linearVelocity.y);
       
        if (state.jump.triggered && state.isGrounded)
        {
            state.TransitionState(state.jumpState);
        }
        else if (state.roll.triggered && state.rb.linearVelocityX != 0)
        {
            state.TransitionState(state.rollState);
        }
        else if (state.block.triggered)
        {
            state.TransitionState(state.blockState);
        }
        else if (state.counter.triggered)
        {
            state.TransitionState(state.counterState);
        }
        else if (xInput == 0)
        {
            state.TransitionState(state.idleState);
        }
        else if(state.rb.linearVelocityY < 0)
        {
            state.TransitionState(state.fallingState);
        }
        else if (state.rb.linearVelocity.x > 0)
        {
            state.SetFacingDirection(true);
        }
        else
        {
            state.SetFacingDirection(false);
        }

    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
    }

  
}