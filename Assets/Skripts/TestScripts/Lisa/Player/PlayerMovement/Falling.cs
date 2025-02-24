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
        //context.IdleCollider.enabled = false;
        //context.mainCollider.enabled = true;
        context.inputActions.Test.Jump.Disable();
    }

    public override void UpdateState()
    {
        
        xInput = context.playerControls.Walk.ReadValue<float>();
        context.rb.linearVelocity = new Vector2(xInput * context.playerStats.walkingSpeed, context.rb.linearVelocity.y);
        if (context.rb.linearVelocityX < 0)
        {
            context.SetFacingDirection(false);
        }
        if (context.rb.linearVelocityX > 0)
        {
            context.SetFacingDirection(true);
        }
        CheckSwitchStates();
    }
    public override void InitializeSubState(){}
    public override void CheckSwitchStates()
    {
  
        if (context.playerControls.Block.triggered)
        {
            SwitchState(factory.Blocking());
        }
        else if (context.playerControls.AirCounter.triggered && context.isEnemy)
        {
            SwitchState(factory.AirCountering());
        }
    }
    public override void ExitState() 
    {
        //context.mainCollider.enabled = true;
        //context.IdleCollider.enabled = false;
        
    }
}