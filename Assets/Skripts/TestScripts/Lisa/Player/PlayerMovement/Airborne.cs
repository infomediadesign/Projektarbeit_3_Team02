using UnityEngine;

public class Airborne : BaseState
{
    public Airborne(StateManager currentContext, StateFactory factory)
    : base(currentContext, factory)
    {
        isRootState = true;
        InitializeSubState();
    }
    public BaseState GetCurrentSubState()
    {
        return currentSubState;
    }
    public override void EnterState()
    {
        Debug.Log("Entering Airborne State");
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        //ApplyAirControl(); 
    }

    public override void CheckSwitchStates()
    {
        if (context.isGrounded && context.playerControls.Jump.phase != UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            SwitchState(factory.Ground());
        }
    }

    public override void InitializeSubState()
    {
        if(context.rb.linearVelocityY < 0)
        {
            SetSubState(factory.Fall());
        }
        else
        {
            SetSubState(factory.Jumping());
        }
    }

    private void ApplyAirControl()
    {
        float xInput = context.playerControls.Walk.ReadValue<float>();
        context.rb.linearVelocity = new Vector2(xInput * context.playerStats.walkingSpeed, context.rb.linearVelocity.y);
    }

    public override void ExitState()
    {
       
    }
}

