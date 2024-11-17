
using UnityEngine;


public class AirCounter : BaseState
{
    private float xInput;
    private bool hasCountered;
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering airCounter state");
        hasCountered = false;
    }


    override public void UpdateState(StateManager state)
    {
        Debug.Log("airCounter");

        xInput = Input.GetAxisRaw("Horizontal");
        state.rb.linearVelocity = new Vector2(xInput * state.walkingSpeed, state.rb.linearVelocity.y);

        if (Input.GetKey(KeyCode.Space) && state.isEnemy && hasCountered == false)
        {
            state.rb.linearVelocity = new Vector2(state.rb.linearVelocity.x, state.jumpForce);
        }
        else if (!state.isEnemy)
        {
            hasCountered = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && state.rb.linearVelocity.y > 0)
        {
            state.rb.linearVelocity = new Vector2(state.rb.linearVelocity.x, state.rb.linearVelocity.y * state.jumpMultiplier);
            hasCountered = true;
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
