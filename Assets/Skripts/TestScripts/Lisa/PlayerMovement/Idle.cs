
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
        xInput = state.walk.ReadValue<float>();

        if (xInput != 0)
        {
            state.TransitionState(state.walkState);
        }
     
        else if (state.jump.triggered && state.isGrounded)
        {
            state.TransitionState(state.jumpState);
        }
        else if (state.counter.triggered)
        {
            state.TransitionState(state.counterState);
        }
        else if (state.block.triggered)
        {
            state.TransitionState(state.blockState);
        }
    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
    }
}
