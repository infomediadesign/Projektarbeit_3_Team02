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
        context.mainCollider.enabled = false;
        context.rollTrigger.enabled = true;
    }

    override public void UpdateState()
    {
        float distanceTraveledRight = Mathf.Abs(context.transform.position.x - lastFramePositionX);
        float distanceTraveledLeft = Mathf.Abs(lastFramePositionX - context.transform.position.x);

        if (FacingRight)
        {
        
  
            if (ShouldContinueRolling()) 
            {
                context.rb.linearVelocity = new Vector2(context.playerStats.rollSpeed, context.rb.linearVelocity.y);
                lastFramePositionX = context.transform.position.x;
                if (distanceTraveledRight <= 0.0001f)
                {
                    SwitchState(factory.Walking());
                    return;
                }
            }
            else
            {
                context.rb.linearVelocity = new Vector2(0f, context.rb.linearVelocity.y);
                SwitchState(factory.Walking());
            }
        }
        else
        {
         
            if (ShouldContinueRolling())
            {
                context.rb.linearVelocity = new Vector2(-context.playerStats.rollSpeed, context.rb.linearVelocity.y);
                lastFramePositionX = context.transform.position.x;
                if (distanceTraveledLeft <= 0.0001f)
                {
                    SwitchState(factory.Walking());
                    return;
                }
            }
            else
            {
                context.rb.linearVelocity = new Vector2(0f, context.rb.linearVelocity.y);
                SwitchState(factory.Walking());
            }
        }
    }

    private bool ShouldContinueRolling()
    {
        float distanceRolled = Mathf.Abs(context.transform.position.x - startPositionX);
        bool withinNormalRollDistance = distanceRolled < context.playerStats.rollDistance;

        bool isLowCeiling = IsSpaceAbove();

        return withinNormalRollDistance || isLowCeiling;
    }

    private bool IsSpaceAbove()
    {
        Vector2 rollPosition = (Vector2)context.transform.position + context.rollTrigger.offset;
        float raycastLength = 0.3f; 
        RaycastHit2D hit = Physics2D.Raycast(rollPosition, Vector2.up, raycastLength, context.groundLayer);

        Debug.DrawRay(rollPosition, Vector2.up * raycastLength, Color.red);

        return hit.collider != null;
    }
    public override void InitializeSubState(){}

    public override void CheckSwitchStates()
    {

    }
    override public void ExitState()
    {
        Debug.Log("exiting roll state");
        context.rollTrigger.enabled = false;
        context.mainCollider.enabled = true;
    }
}

