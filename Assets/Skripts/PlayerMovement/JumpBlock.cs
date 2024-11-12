
using UnityEngine;

public class JumpBlock : BaseState
{
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering jumpblock state");
    }


    override public void UpdateState(StateManager state)
    {
        Debug.Log("jumpblock");

        if (state.isGrounded)
        {
            state.TransitionState(state.walkState);
        }
      

    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
    }
}

