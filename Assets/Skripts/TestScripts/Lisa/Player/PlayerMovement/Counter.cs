
using UnityEngine;

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

    }
   
    override public void UpdateState()
    {
        if (!attacked)
        {
            enemy = context.zone.GetCounterableEnemy();

            
                if (enemy != null)
                {
                    context.playerCombat.Attack(enemy);
                    Debug.Log("Counter Successful!");
                    if (context.counterPossible)
                    {
                        SwitchState(factory.Idleing());
                        attacked = true;
                    }
                    
                }
                else
                {
                    Debug.LogError("Counter not Successful!");
                    if (context.counterPossible)
                    {
                        SwitchState(factory.Idleing());
                    }
                    
                }
            
            
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
