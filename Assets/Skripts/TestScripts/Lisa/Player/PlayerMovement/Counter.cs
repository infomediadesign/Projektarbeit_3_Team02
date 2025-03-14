
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class Counter : BaseState
{
    public Counter(StateManager currentContext, StateFactory factory)
    : base(currentContext, factory) { }
    private bool attacked;



    EnemyBase enemy;
    override public void EnterState()
    {  
        attacked = false;
        enemy = context.zone.GetCounterableEnemy();
       

        if (enemy == null)
        {
            Debug.LogError("no enemy!");
        }
        soundStop = false;
    }
   
    override public void UpdateState()
    {
        if (!soundStop)
        {
            PlaySound("MCCounter");
            soundStop = true;
        }
        if (!attacked)
        {
            enemy = context.zone.GetCounterableEnemy();

            
                if (enemy != null && context.counterWindow)
                {
                    context.playerCombat.Attack(enemy);
                    context.StartCoroutine(context.FreezeAnimation(0.2f));
                   
                    if (context.counterPossible)
                    {
                    Debug.Log("Counter Successful!");
                    SwitchState(factory.Idleing());
                        attacked = true;
                    }
                    
                }
                else
                {
                    
                    if (context.counterPossible)
                    {
                    Debug.Log("Counter not Successful!");
                    SwitchState(factory.Idleing());
                    }
                    
                }
            
            
        }
        else if (context.isObstacle)
        {
            context.playerCombat.Attack(enemy);
        }
    }
    public override void InitializeSubState(){}
    public override void CheckSwitchStates()
    {
    }
    override public void ExitState()
    {
    }
  
}
