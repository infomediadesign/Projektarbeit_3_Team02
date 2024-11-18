using UnityEngine;

public class Walk : BaseState
{

    private float xInput;
   

  
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering walking state");
   
    }


    override public void UpdateState(StateManager state)
    {
        
       // xInput = Input.GetAxisRaw("Horizontal");
        xInput = state.walk.ReadValue<float>();
        state.rb.linearVelocity = new Vector2(xInput * state.walkingSpeed, state.rb.linearVelocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && state.isGrounded)
        {
            state.TransitionState(state.jumpState);
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.R))
        {
            state.TransitionState(state.rollState);
        }
        else if (Input.GetKey(KeyCode.B))
        {
            state.TransitionState(state.blockState);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            state.TransitionState(state.counterState);
        }
        else if (xInput == 0)
        {
            state.TransitionState(state.idleState);
        }
    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
    }

  
}
