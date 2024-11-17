using UnityEngine;

public class Jump : BaseState
{
    private float xInput;
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering jump state");
       
       
    }


    override public void UpdateState(StateManager state)
    {
        xInput = Input.GetAxisRaw("Horizontal");
        state.rb.linearVelocity = new Vector2(xInput * state.walkingSpeed, state.rb.linearVelocity.y);

        if (Input.GetKey(KeyCode.Space) && state.isGrounded)
        {
            state.rb.linearVelocity = new Vector2(state.rb.linearVelocity.x, state.jumpForce);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && state.rb.linearVelocity.y > 0)
        {
            state.rb.linearVelocity = new Vector2(state.rb.linearVelocity.x, state.rb.linearVelocity.y * state.jumpMultiplier);
        }


        else if (Input.GetKeyDown(KeyCode.Space) && !state.isGrounded && state.isEnemy)
        {
            state.TransitionState(state.airCounterState);
        }
        else if (Input.GetKey(KeyCode.B))
        {
            state.TransitionState(state.jumpBlockState);
        }
        else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.A)) //damit nicht gerollt wird, wenn der spieler nur nach oben springt
        {
            state.TransitionState(state.rollState);
        }
        else if (state.isGrounded)
        {
            state.TransitionState(state.walkState);
        }
    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
    }
}
