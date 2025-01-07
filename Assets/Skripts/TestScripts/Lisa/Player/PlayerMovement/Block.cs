
using UnityEngine;

public class Block : BaseState
{
    public Block(StateManager currentContext, StateFactory factory)
    : base(currentContext, factory) { }
    SpriteRenderer renderer;
    private bool blockReleased;
    override public void EnterState()
    {
        Debug.Log("entering Block state");
        renderer = context.GetComponent<SpriteRenderer>();
        context.rb.linearVelocity = new Vector2(0, 0);
        context.playerControls.Block.canceled += OnBlockCanceled;
        blockReleased = false;


    }


    override public void UpdateState()
    {
        Debug.Log("block");
        renderer.color = Color.green;


        if (blockReleased)
        {
            renderer.color = Color.white;
            //state.TransitionState(state.walkState);
            SwitchState(factory.Idleing());
        }
       
    }
    public override void InitializeSubState()
    {

    }
    public override void CheckSwitchStates()
    {
    }
    override public void ExitState()
    {
        Debug.Log("exiting state");
        context.playerControls.Block.canceled -= OnBlockCanceled;
    }
    private void OnBlockCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        blockReleased = true;
    }
}

