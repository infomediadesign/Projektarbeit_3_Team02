using UnityEngine;

public class Falling : BaseState
{
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering fall state");
    }

    public override void UpdateState(StateManager state)
    {
        
        if(state.isGrounded && state.rb.linearVelocityX != 0)
        {
            state.TransitionState(state.walkState);
        }
        else if(state.isGrounded && state.rb.linearVelocityX == 0) 
        {
            state.TransitionState(state.idleState);
        }
        else if(state.block.triggered)
        {
            state.TransitionState(state.jumpBlockState);
        }
        else if (state.airCounter.triggered)
        {
            state.TransitionState(state.airCounterState);
        }
    }

    public override void ExitState(StateManager state) 
    {
        Debug.Log("exiting fall state");
    }
}