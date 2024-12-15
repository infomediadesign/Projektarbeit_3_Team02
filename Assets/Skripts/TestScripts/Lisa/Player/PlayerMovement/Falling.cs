using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.Windows;

public class Falling : BaseState
{   
    public Falling(StateManager currentContext, StateFactory factory)
    : base(currentContext, factory) { }
    private float xInput;
    override public void EnterState()
    {
    }

    public override void UpdateState()
    {
        xInput = context.playerControls.Walk.ReadValue<float>();
        context.rb.linearVelocity = new Vector2(xInput * context.playerStats.walkingSpeed, context.rb.linearVelocity.y);
        if (context.rb.linearVelocityX < 0)
        {
            context.SetFacingDirection(false);
        }
        else
        {
            context.SetFacingDirection(true);
        }
    
}
    public override void InitializeSubState(){}
    public override void CheckSwitchStates()
    {
        if (context.rb.linearVelocityX != 0)
        {
            //state.TransitionState(state.walkState);
            SwitchState(factory.Walking());
        }
        else if (context.rb.linearVelocityX == 0)
        {
            //state.TransitionState(state.idleState);
            SwitchState(factory.Idleing());
        }
        else if (context.playerControls.Block.triggered)
        {
            //state.TransitionState(state.jumpBlockState);
            SwitchState(factory.Blocking());
        }
        else if (context.playerControls.AirCounter.triggered)
        {
            //state.TransitionState(state.airCounterState);
            SwitchState(factory.AirCountering());
        }
    }
    public override void ExitState() 
    {
        Debug.Log("exiting fall state");
    }
}