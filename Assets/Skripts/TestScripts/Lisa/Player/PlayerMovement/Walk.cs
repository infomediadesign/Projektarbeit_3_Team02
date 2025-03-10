using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
public class Walk : BaseState
{
    public Walk(StateManager currentContext, StateFactory factory)
    : base(currentContext, factory) { }
    private float xInput;
    private float soundTimer = 0f;  // Timer für den Intervall
    private float soundInterval = 5f;
    override public void EnterState()
    {
        soundStop = false;
    }

    override public void UpdateState()
    {
 
        xInput = context.playerControls.Walk.ReadValue<float>();
        context.rb.linearVelocity = new Vector2(xInput * context.playerStats.walkingSpeed, context.rb.linearVelocity.y);

        if (context.rb.linearVelocity.x > 0)
        {
            context.SetFacingDirection(true);
        }
        if (context.rb.linearVelocity.x < 0)
        {
            context.SetFacingDirection(false);
        }
        if(context.rb.linearVelocityX != 0 && context.rb.linearVelocityY == 0)
        {
            if (!soundStop)
            {
                PlaySound("MCRunSound");
            }
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

        else if (!context.playerControls.Walk.IsPressed())
        {
            SwitchState(factory.Idleing());
        }
        
    }


    override public void ExitState()
    {
        soundStop = true;
        SoundManager.Instance.StopPlayerSound2D();
        Debug.Log("sound stopped");
        Debug.Log("exiting state");
    }
  
}