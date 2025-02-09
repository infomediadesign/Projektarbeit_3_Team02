
using UnityEngine;

public class Block : BaseState
{
    public Block(StateManager currentContext, StateFactory factory)
    : base(currentContext, factory) { }
    SpriteRenderer renderer;
    private bool blockReleased;
    private Vector2 linearVel;
    override public void EnterState()
    {

        renderer = context.GetComponent<SpriteRenderer>();
        linearVel = context.rb.linearVelocity;
        context.rb.linearVelocity = new Vector2(0, 0);

        context.playerControls.Block.canceled += OnBlockCanceled;
        blockReleased = false;


    }


    override public void UpdateState()
    {
        Debug.Log("block");
        renderer.color = Color.green;


        /*if (blockReleased)
        {
            renderer.color = Color.white;
            context.rb.linearVelocity = linearVel;
            //state.TransitionState(state.walkState);
            SwitchState(factory.Idleing());
        }*/
        if(context.isGrounded)
        {
            renderer.color = Color.white;
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

        context.playerControls.Block.canceled -= OnBlockCanceled;
    }
    private void OnBlockCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        blockReleased = true;
    }
}

