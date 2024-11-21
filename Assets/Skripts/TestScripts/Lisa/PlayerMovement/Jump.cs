using UnityEngine;

public class Jump : BaseState
{
    private float xInput;
    private bool jumpReleased;
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering jump state");
        state.jump.canceled += OnJumpCanceled;       
    }
    override public void UpdateState(StateManager state)
    {
        xInput = state.walk.ReadValue<float>();
        state.rb.linearVelocity = new Vector2(xInput * state.walkingSpeed, state.rb.linearVelocity.y);

        if (state.isGrounded)
        {
            state.rb.linearVelocity = new Vector2(state.rb.linearVelocity.x, state.jumpForce);
        }
        else if (jumpReleased && state.rb.linearVelocity.y > 0)
        {
            state.rb.linearVelocity = new Vector2(state.rb.linearVelocity.x, state.rb.linearVelocity.y * state.jumpMultiplier);
            jumpReleased = false;
        }
        else if(state.rb.linearVelocityY < 0)
        {
            state.TransitionState(state.fallingState);
        }


        else if (state.airCounter.triggered && !state.isGrounded && state.isEnemy)
        {
            state.TransitionState(state.airCounterState);
        }
        else if (state.block.triggered)
        {
            state.TransitionState(state.jumpBlockState);
        }
        else if (state.roll.triggered && state.rb.linearVelocityX != 0)
        {
            state.TransitionState(state.rollState);
        }
        else if (state.isGrounded)
        {
            state.TransitionState(state.walkState);
        }

        if(state.rb.linearVelocityX < 0)
        {
            state.SetFacingDirection(false);
        }
        else
        {
            state.SetFacingDirection(true);
        }
    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
        state.jump.canceled -= OnJumpCanceled;
    }
    private void OnJumpCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        jumpReleased = true;
    }
}
