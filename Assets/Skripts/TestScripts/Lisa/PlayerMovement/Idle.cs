
using UnityEngine;

public class Idle : BaseState
{
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering idle state");
    }


    override public void UpdateState(StateManager state)
    {
        Debug.Log("idle");
        state.rb.linearVelocity = new Vector2(0,0);
        
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            state.TransitionState(state.walkState);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && state.isGrounded)
        {
            state.TransitionState(state.jumpState);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            state.TransitionState(state.counterState);
        }
    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
    }
}
