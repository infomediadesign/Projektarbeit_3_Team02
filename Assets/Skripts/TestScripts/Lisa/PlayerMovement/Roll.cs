using UnityEngine;

public class Roll : BaseState
{
    private bool FacingRight;
    private float startPositionX; 

    override public void EnterState(StateManager state)
    {
        Debug.Log("entering roll state");
        startPositionX = state.transform.position.x;

        if (Input.GetKey(KeyCode.A))
        {
            FacingRight = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            FacingRight = true;
        }
        //je anchdem ob facing right oder nicht wird rollspeed negativ oder positiv gesetzt yay
        state.rb.linearVelocity = new Vector2(FacingRight ? state.rollSpeed : -state.rollSpeed, state.rb.linearVelocity.y);
        state.capCol.size = new Vector2(1, 0.1f);
      
    }

    override public void UpdateState(StateManager state)
    {
        Debug.Log("rolling");

        if (FacingRight)
        {
            if (state.transform.position.x - startPositionX < state.rollDistance) //oder collision mit tag "decke"
            {
                state.rb.linearVelocity = new Vector2(state.rollSpeed, state.rb.linearVelocity.y);
            }
            else
            {
                state.rb.linearVelocity = new Vector2(0f, state.rb.linearVelocity.y);
                state.TransitionState(state.walkState); 
            }
        }
        else
        {
            if (startPositionX - state.transform.position.x < state.rollDistance)
            {
                state.rb.linearVelocity = new Vector2(-state.rollSpeed, state.rb.linearVelocity.y);
            }
            else
            {
                state.rb.linearVelocity = new Vector2(0f, state.rb.linearVelocity.y);
                state.TransitionState(state.walkState);
            }
        }
    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting roll state");
        state.capCol.size = new Vector2(1, 1.5f);
    }
}

