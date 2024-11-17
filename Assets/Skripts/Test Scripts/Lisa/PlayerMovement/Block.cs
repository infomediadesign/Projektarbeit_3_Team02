
using UnityEngine;

public class Block : BaseState
{
    SpriteRenderer renderer;
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering Block state");
        renderer = state.GetComponent<SpriteRenderer>();
        state.rb.linearVelocity = new Vector2(0, 0);
        
    }


    override public void UpdateState(StateManager state)
    {
        Debug.Log("block");
        renderer.color = Color.green;


        if (Input.GetKeyUp(KeyCode.B))
        {
            renderer.color = Color.white;
            state.TransitionState(state.walkState);
        }
    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
        
    }
}

