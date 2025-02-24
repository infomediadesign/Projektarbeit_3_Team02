
using UnityEngine;


public class AirCounter : BaseState
{
    public AirCounter(StateManager currentContext, StateFactory factory)
    : base(currentContext, factory) { }
    private float xInput;
    private bool airCounterReleased;
    private bool airCounterPressed;
    private bool attacked;
    override public void EnterState()
    {

        context.playerControls.AirCounter.canceled += OnAirCounterCanceled;
        context.playerControls.AirCounter.performed += OnAirCounterPressed;
        airCounterReleased = false;
        airCounterPressed = true;
        attacked = false;
    }


    override public void UpdateState()
    {

        xInput = context.playerControls.Walk.ReadValue<float>();
        context.rb.linearVelocity = new Vector2(xInput * context.playerStats.walkingSpeed, context.rb.linearVelocity.y);

        if (context.isEnemy && airCounterPressed)
        {
            context.rb.linearVelocity = new Vector2(context.rb.linearVelocity.x, context.playerStats.airCounterForce);
            airCounterPressed = false;
            attacked = false;
        }
        else if (airCounterReleased && context.rb.linearVelocity.y > 0)
        {
            context.rb.linearVelocity = new Vector2(context.rb.linearVelocity.x, context.rb.linearVelocity.y * context.playerStats.airCounterMultiplier);
            airCounterReleased = false;
        }

        if (context.rb.linearVelocityX < 0)
        {
            context.SetFacingDirection(false);
        }
        if (context.rb.linearVelocityX > 0)
        {
            context.SetFacingDirection(true);
        }

        if (!attacked && context.CheckForEnemy())
        {
            context.playerCombat.Attack(context.currentEnemy);
            attacked = true;
        }
        CheckSwitchStates();

    }
    public override void InitializeSubState()
    {
    }
    public override void CheckSwitchStates()
    {
        if (context.playerControls.Block.triggered)
        {
            SwitchState(factory.Blocking());
        }
        else if(context.rb.linearVelocityY < 0)
        {
            SwitchState(factory.Fall());
        }
    }
    override public void ExitState()
    {

        attacked = false;
        context.playerControls.AirCounter.canceled -= OnAirCounterCanceled;
    }
    private void OnAirCounterCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        airCounterReleased = true;
        airCounterPressed = false;
    }
    private void OnAirCounterPressed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        airCounterReleased = false;
        airCounterPressed = true;
    }
}
