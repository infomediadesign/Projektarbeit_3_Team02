
using UnityEngine;


public class AirCounter : BaseState
{
    private float xInput;
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering airCounter state");
    }


    override public void UpdateState(StateManager state)
    {
        Debug.Log("airCounter");

        xInput = Input.GetAxisRaw("Horizontal");
        state.rb.linearVelocity = new Vector2(xInput * state.walkingSpeed, state.rb.linearVelocity.y);

        if (Input.GetKey(KeyCode.Space) && state.isEnemy)
        {
            state.rb.linearVelocity = new Vector2(state.rb.linearVelocity.x, state.jumpForce);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && state.rb.linearVelocity.y > 0)
        {
            state.rb.linearVelocity = new Vector2(state.rb.linearVelocity.x, state.rb.linearVelocity.y * state.jumpMultiplier);
        }


        if (state.isGrounded)
        {
            state.TransitionState(state.walkState);
        }

        if (Input.GetKey(KeyCode.B))
        {
            state.TransitionState(state.jumpBlockState);
        }
       
        
    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
    }
}
