
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class Idle : BaseState
{
    public Idle(StateManager currentContext, StateFactory factory)
    : base(currentContext, factory) { }
    private float xInput;
    override public void EnterState()
    {
        Debug.Log("entering idle state");
    }
    override public void UpdateState()
    {
        CheckSwitchStates();
        Debug.Log("UPDATING IDLE STATE");
        context.rb.linearVelocity = new Vector2(0, 0);
       
        xInput = context.playerControls.Walk.ReadValue<float>();
        if (xInput != 0)
        {
            Debug.Log("INPUT NICHT NULL");
            SwitchState(factory.Walking());
        }
        else if (context.playerControls.Counter.triggered)
        {
            SwitchState(factory.Countering());
        }
        else if (context.playerControls.Block.IsPressed())
        {
            SwitchState(factory.Blocking());
        }
    }
    public override void InitializeSubState(){}
    public override void CheckSwitchStates()
    {
        if (xInput != 0)
        {
            Debug.Log("INPUT NICHT NULL");
            SwitchState(factory.Walking());
        }
        else if (context.playerControls.Counter.triggered)
        {
            SwitchState(factory.Countering());
        }
        else if (context.playerControls.Block.IsPressed())
        {
            SwitchState(factory.Blocking());
        }
    }
    override public void ExitState()
    {
        Debug.Log("exiting state");
    }
}
