
using UnityEngine;


public class AirCounter : BaseState
{
    private float xInput;
    private bool airCounterReleased;
    private bool airCounterPressed;
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering airCounter state");
        state.playerControls.AirCounter.canceled += OnAirCounterCanceled;
        state.playerControls.AirCounter.performed += OnAirCounterPressed;
        airCounterReleased = false;
        airCounterPressed = true;
    }


    override public void UpdateState(StateManager state)
    {
        Debug.Log("airCounter");

        xInput = state.playerControls.Walk.ReadValue<float>();
        state.rb.linearVelocity = new Vector2(xInput * state.playerStats.walkingSpeed, state.rb.linearVelocity.y);

        if (state.isEnemy && airCounterPressed)
        {
            state.rb.linearVelocity = new Vector2(state.rb.linearVelocity.x, state.playerStats.airCounterForce);
            airCounterPressed = false;
        }
        else if (airCounterReleased && state.rb.linearVelocity.y > 0)
        {
            state.rb.linearVelocity = new Vector2(state.rb.linearVelocity.x, state.rb.linearVelocity.y * state.playerStats.airCounterMultiplier);
            airCounterReleased = false;
        }
       


        if (state.isGrounded)
        {
            state.TransitionState(state.walkState);
        }

        if (state.playerControls.Block.triggered)
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
        state.playerControls.AirCounter.canceled -= OnAirCounterCanceled;
    }
    private void OnAirCounterCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        airCounterReleased = true;
        airCounterPressed = false;
    }
    private void OnAirCounterPressed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        airCounterReleased = false;
        airCounterPressed = true;
    }
}
