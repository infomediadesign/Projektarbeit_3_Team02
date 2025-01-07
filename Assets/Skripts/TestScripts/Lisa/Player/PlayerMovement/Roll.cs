using UnityEngine;

public class Roll : BaseState
{
    public Roll(StateManager currentContext, StateFactory factory)
   : base(currentContext, factory) { }

    private bool FacingRight;
    private float startPositionX;
    private Vector2 originOffset;
    private float lastFramePositionX;

    override public void EnterState()
    {
        startPositionX = context.transform.position.x;
        originOffset = context.capCol.offset;

        if (context.rb.linearVelocityX < 0)
        {
            FacingRight = false;
        }
        else if (context.rb.linearVelocityX > 0)
        {
            FacingRight = true;
        }
        //je anchdem ob facing right oder nicht wird rollspeed negativ oder positiv gesetzt 
        context.rb.linearVelocity = new Vector2(FacingRight ? context.playerStats.rollSpeed : -context.playerStats.rollSpeed, context.rb.linearVelocity.y);
        context.capCol.size = new Vector2(1, 0.3f);
        context.capCol.offset = new Vector2(0, -0.6f);
    }

    override public void UpdateState()
    {
        float distanceTraveledRight = Mathf.Abs(context.transform.position.x - lastFramePositionX);
        float distanceTraveledLeft = Mathf.Abs(lastFramePositionX - context.transform.position.x);

        if (FacingRight)
        {
  
            if (context.transform.position.x - startPositionX < context.playerStats.rollDistance) 
            {
                context.rb.linearVelocity = new Vector2(context.playerStats.rollSpeed, context.rb.linearVelocity.y);
                lastFramePositionX = context.transform.position.x;
                if (distanceTraveledRight <= 0.001f)
                {
                    //context.TransitionState(state.walkState);
                    SwitchState(factory.Walking());
                    return;
                }
            }
            else
            {
                context.rb.linearVelocity = new Vector2(0f, context.rb.linearVelocity.y);
                //state.TransitionState(state.walkState); 
                SwitchState(factory.Walking());
            }
        }
        else
        {
         
            if (startPositionX - context.transform.position.x < context.playerStats.rollDistance)
            {
                context.rb.linearVelocity = new Vector2(-context.playerStats.rollSpeed, context.rb.linearVelocity.y);
                lastFramePositionX = context.transform.position.x;
                if (distanceTraveledLeft <= 0.001f)
                {
                    //state.TransitionState(state.walkState);
                    SwitchState(factory.Walking());
                    return;
                }
            }
            else
            {
                context.rb.linearVelocity = new Vector2(0f, context.rb.linearVelocity.y);
                //state.TransitionState(state.walkState);
                SwitchState(factory.Walking());
            }
        }
    }
    public override void InitializeSubState(){}

    public override void CheckSwitchStates()
    {

    }
    override public void ExitState()
    {
        Debug.Log("exiting roll state");
        context.capCol.size = new Vector2(1, 1.5f);
        context.capCol.offset = originOffset;
    }
}

