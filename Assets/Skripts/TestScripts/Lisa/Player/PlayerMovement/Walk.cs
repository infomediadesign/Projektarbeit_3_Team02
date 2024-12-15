using UnityEngine;

public class Walk : BaseState
{
    public Walk(StateManager currentContext, StateFactory factory)
    : base(currentContext, factory) { }
    private float xInput;

    override public void EnterState()
    {
        Debug.Log("entering walking state...");  
    }

    override public void UpdateState()
    {
        xInput = context.playerControls.Walk.ReadValue<float>();
        context.rb.linearVelocity = new Vector2(xInput * context.playerStats.walkingSpeed, context.rb.linearVelocity.y);
               
        if (context.rb.linearVelocity.x > 0)
        {
            context.SetFacingDirection(true);
        }
        else
        {
            context.SetFacingDirection(false);
        }
        CheckSwitchStates();

    }
    public override void InitializeSubState(){}

    public override void CheckSwitchStates()
    {

        if (context.playerControls.Roll.triggered && context.rb.linearVelocityX != 0)
        {
            SwitchState(factory.Rolling());
        }
        else if (context.playerControls.Block.triggered)
        {
            SwitchState(factory.Blocking());
        }
        else if (context.playerControls.Counter.triggered)
        {
            SwitchState(factory.Countering());
        }
        else if (xInput == 0)
        {
            SwitchState(factory.Idleing());
        }
        else if (context.rb.linearVelocityY < 0)
        {
            SwitchState(factory.Fall());
        }
    }
    override public void ExitState()
    {
        Debug.Log("exiting state");
    }

  
}