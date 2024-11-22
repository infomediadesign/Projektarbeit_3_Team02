using UnityEngine;

public class Roll : BaseState
{
    private bool FacingRight;
    private float startPositionX;
    private Vector2 originOffset;
    private float lastFramePositionX;

    override public void EnterState(StateManager state)
    {
        Debug.Log("entering roll state");
        startPositionX = state.transform.position.x;
        originOffset = state.capCol.offset;

        if (state.rb.linearVelocityX < 0)
        {
            FacingRight = false;
        }
        else if (state.rb.linearVelocityX > 0)
        {
            FacingRight = true;
        }
        //je anchdem ob facing right oder nicht wird rollspeed negativ oder positiv gesetzt 
        state.rb.linearVelocity = new Vector2(FacingRight ? state.playerStats.rollSpeed : -state.playerStats.rollSpeed, state.rb.linearVelocity.y);
        state.capCol.size = new Vector2(1, 0.3f);
        state.capCol.offset = new Vector2(0, -0.6f);
    }

    override public void UpdateState(StateManager state)
    {
        float distanceTraveledRight = Mathf.Abs(state.transform.position.x - lastFramePositionX);
        float distanceTraveledLeft = Mathf.Abs(lastFramePositionX - state.transform.position.x);

        if (FacingRight)
        {
  
            if (state.transform.position.x - startPositionX < state.playerStats.rollDistance) 
            {
                state.rb.linearVelocity = new Vector2(state.playerStats.rollSpeed, state.rb.linearVelocity.y);
                lastFramePositionX = state.transform.position.x;
                if (distanceTraveledRight <= 0.001f)
                {
                    state.TransitionState(state.walkState);
                    return;
                }
            }
            else
            {
                state.rb.linearVelocity = new Vector2(0f, state.rb.linearVelocity.y);
                state.TransitionState(state.walkState); 
            }
        }
        else
        {
         
            if (startPositionX - state.transform.position.x < state.playerStats.rollDistance)
            {
                state.rb.linearVelocity = new Vector2(-state.playerStats.rollSpeed, state.rb.linearVelocity.y);
                lastFramePositionX = state.transform.position.x;
                if (distanceTraveledLeft <= 0.001f)
                {
                    state.TransitionState(state.walkState);
                    return;
                }
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
        state.capCol.offset = originOffset;
    }
}

