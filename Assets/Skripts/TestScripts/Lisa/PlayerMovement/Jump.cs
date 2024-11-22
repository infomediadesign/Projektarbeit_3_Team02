using UnityEngine;

public class Jump : BaseState
{
    private float xInput;
    private bool jumpReleased;
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering jump state");
        state.playerControls.Jump.canceled += OnJumpCanceled;       
    }
    override public void UpdateState(StateManager state)
    {
        xInput = state.playerControls.Walk.ReadValue<float>();
        state.rb.linearVelocity = new Vector2(xInput * state.playerStats.walkingSpeed, state.rb.linearVelocity.y);

        if (state.isGrounded)
        {
            state.rb.linearVelocity = new Vector2(state.rb.linearVelocity.x, state.playerStats.jumpForce);
        }
        else if (jumpReleased && state.rb.linearVelocity.y > 0)
        {
            state.rb.linearVelocity = new Vector2(state.rb.linearVelocity.x, state.rb.linearVelocity.y * state.playerStats.jumpMultiplier);
            jumpReleased = false;
        }
        else if(state.rb.linearVelocityY < 0)
        {
            state.TransitionState(state.fallingState);
        }


        else if (state.playerControls.AirCounter.triggered && !state.isGrounded && state.isEnemy)
        {
            state.TransitionState(state.airCounterState);
        }
        else if (state.playerControls.Block.triggered)
        {
            state.TransitionState(state.jumpBlockState);
        }
        else if (state.playerControls.Roll.triggered && state.rb.linearVelocityX != 0)
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
        state.playerControls.Jump.canceled -= OnJumpCanceled;
    }
    private void OnJumpCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        jumpReleased = true;
    }
}
