
using UnityEngine;

public class Block : BaseState
{
    SpriteRenderer renderer;
    private bool blockReleased;
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering Block state");
        renderer = state.GetComponent<SpriteRenderer>();
        state.rb.linearVelocity = new Vector2(0, 0);
        state.block.canceled += OnBlockCanceled;
        blockReleased = false;


    }


    override public void UpdateState(StateManager state)
    {
        Debug.Log("block");
        renderer.color = Color.green;


        if (blockReleased)
        {
            renderer.color = Color.white;
            state.TransitionState(state.walkState);
        }
       
    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
        state.block.canceled -= OnBlockCanceled;
    }
    private void OnBlockCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        blockReleased = true;
    }
}

