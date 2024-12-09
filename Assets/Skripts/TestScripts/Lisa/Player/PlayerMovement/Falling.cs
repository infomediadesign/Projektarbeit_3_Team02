using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.Windows;

public class Falling : BaseState
{
    private float xInput;
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering fall state");
    }

    public override void UpdateState(StateManager state)
    {
        xInput = state.playerControls.Walk.ReadValue<float>();
        state.rb.linearVelocity = new Vector2(xInput * state.playerStats.walkingSpeed, state.rb.linearVelocity.y);
        if (state.isGrounded && state.rb.linearVelocityX != 0)
        {
            state.TransitionState(state.walkState);
        }
        else if(state.isGrounded && state.rb.linearVelocityX == 0) 
        {
            state.TransitionState(state.idleState);
        }
        else if(state.playerControls.Block.triggered)
        {
            state.TransitionState(state.jumpBlockState);
        }
        else if (state.playerControls.AirCounter.triggered)
        {
            state.TransitionState(state.airCounterState);
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

    public override void ExitState(StateManager state) 
    {
        Debug.Log("exiting fall state");
    }
}