
using UnityEngine;


public class AirCounter : BaseState
{
    private float xInput;
    private bool hasCountered;
    private bool airCounterReleased;
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering airCounter state");
        hasCountered = false;
        state.airCounter.canceled += OnAirCounterCanceled;
        airCounterReleased = false;
    }


    override public void UpdateState(StateManager state)
    {
        Debug.Log("airCounter");

        xInput = state.walk.ReadValue<float>();
        state.rb.linearVelocity = new Vector2(xInput * state.walkingSpeed, state.rb.linearVelocity.y);

        if (state.isEnemy && hasCountered == false)
        {
            state.rb.linearVelocity = new Vector2(state.rb.linearVelocity.x, state.jumpForce);
        }
        else if (!state.isEnemy)
        {
            hasCountered = true;
        }
        else if (airCounterReleased && state.rb.linearVelocity.y > 0)
        {
            state.rb.linearVelocity = new Vector2(state.rb.linearVelocity.x, state.rb.linearVelocity.y * state.jumpMultiplier);
            hasCountered = true;
        }


        if (state.isGrounded)
        {
            state.TransitionState(state.walkState);
        }

        if (state.block.triggered)
        {
            state.TransitionState(state.jumpBlockState);
        }
        if (state.rb.linearVelocityX < 0)
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
        state.airCounter.canceled -= OnAirCounterCanceled;
    }
    private void OnAirCounterCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        airCounterReleased = true;
    }
}
