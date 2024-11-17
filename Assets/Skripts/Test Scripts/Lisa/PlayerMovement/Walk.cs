using UnityEngine;

public class Walk : BaseState
{

    private float xInput;
    private float idleTime = 5;
    private float elapsedTime;
  
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering walking state");
        elapsedTime = 0;
    }


    override public void UpdateState(StateManager state)
    {
        elapsedTime += Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        state.rb.linearVelocity = new Vector2(xInput * state.walkingSpeed, state.rb.linearVelocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && state.isGrounded)
        {
            state.TransitionState(state.jumpState);
        }
        else if(elapsedTime >= idleTime)
        {
            state.TransitionState(state.idleState);
        }
        else if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.R))
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
    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
    }

  
}
