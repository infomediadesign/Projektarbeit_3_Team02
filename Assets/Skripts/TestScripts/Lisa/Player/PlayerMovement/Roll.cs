using UnityEngine;

public class Roll : BaseState
{
    public Roll(StateManager currentContext, StateFactory factory)
   : base(currentContext, factory) { }

    private bool FacingRight;
    private float startPositionX;
    private float lastFramePositionX;
    

    override public void EnterState()
    {
        context.animator.SetBool("rollFinished", false);
        startPositionX = context.transform.position.x;

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
        
        context.rollTrigger.enabled = true;
        context.mainCollider.enabled = false;
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
                if (distanceTraveledRight <= 0.001f && !IsSpaceInFront())
                {

                    SwitchState(factory.Walking());
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
                if (distanceTraveledLeft <= 0.001f && !IsSpaceInFront())
                {
                    SwitchState(factory.Walking());
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
        float raycastLength = 0.8f; 
        RaycastHit2D hit = Physics2D.Raycast(rollPosition, Vector2.up, raycastLength, context.groundLayer);

        return hit.collider != null;
    }
    private bool IsSpaceInFront()
    {

        Vector2 direction;
        if (FacingRight)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
        Vector2 origin = (Vector2)context.transform.position + context.rollTrigger.offset;
        float raycastLength = 0.5f;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, raycastLength, context.groundLayer);

        return hit.collider == null;
    }
    public override void InitializeSubState(){}

    public override void CheckSwitchStates()
    {

    }
    override public void ExitState()
    {
        context.mainCollider.enabled = true;
        context.rollTrigger.enabled = false;

        context.animator.SetBool("rollFinished", true);

    }
}

