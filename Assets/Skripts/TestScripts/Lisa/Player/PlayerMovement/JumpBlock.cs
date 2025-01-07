
using UnityEngine;
using UnityEngine.UIElements;

public class JumpBlock : BaseState
{
    public JumpBlock(StateManager currentContext, StateFactory factory)
    : base(currentContext, factory) { }
    SpriteRenderer renderer;
    override public void EnterState()
    {
        Debug.Log("entering jumpblock state");
        renderer = context.GetComponent<SpriteRenderer>();
        context.rb.linearVelocity = new Vector2(0, context.rb.linearVelocity.y);
    }


    override public void UpdateState()
    {
        Debug.Log("jumpblock");
        renderer.color = Color.green;

        /*if (context.isGrounded && context.playerControls.Block.triggered)
        {
            state.TransitionState(state.blockState);
        }
        else if (context.isGrounded)
        {
            state.TransitionState(state.walkState);
            renderer.color = Color.white;
        }*/
    }

    public override void InitializeSubState(){ }

    public override void CheckSwitchStates()
    {
        if (context.playerControls.Block.triggered)
        {
            //state.TransitionState(state.blockState);
            SwitchState(factory.Blocking());
        }
    }
    override public void ExitState()
    {
        Debug.Log("exiting state");
    }
}

