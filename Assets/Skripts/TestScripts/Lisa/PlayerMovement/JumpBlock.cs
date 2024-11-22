
using UnityEngine;
using UnityEngine.UIElements;

public class JumpBlock : BaseState
{
    SpriteRenderer renderer;
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering jumpblock state");
        renderer = state.GetComponent<SpriteRenderer>();
        state.rb.linearVelocity = new Vector2(0, state.rb.linearVelocity.y);
    }


    override public void UpdateState(StateManager state)
    {
        Debug.Log("jumpblock");
        renderer.color = Color.green;

        if (state.isGrounded && state.playerControls.Block.triggered)
        {
            state.TransitionState(state.blockState);
        }
        else if (state.isGrounded)
        {
            state.TransitionState(state.walkState);
            renderer.color = Color.white;
        }
      

    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
    }
}

