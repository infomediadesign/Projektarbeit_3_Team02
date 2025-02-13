
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
        context.animator.SetBool("blockFinished", false);
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

        if(context.isGrounded && blockReleased)
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
        context.animator.SetBool("blockFinished", true);

    }
    private void OnBlockCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        blockReleased = true;
    }
}

