
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class Idle : BaseState
{
    private float xInput;
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering idle state");
    }


    override public void UpdateState(StateManager state)
    {
      
        state.rb.linearVelocity = new Vector2(0,0);
        xInput = state.playerControls.Walk.ReadValue<float>();

        if (xInput != 0)
        {
            state.TransitionState(state.walkState);
        }
     
        else if (state.playerControls.Jump.triggered && state.isGrounded)
        {
            state.TransitionState(state.jumpState);
        }
        else if (state.playerControls.Counter.triggered)
        {
            state.TransitionState(state.counterState);
        }
        else if (state.playerControls.Block.triggered)
        {
            state.TransitionState(state.blockState);
        }
    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
    }
}
