
using UnityEngine;

public class Counter : BaseState
{
    public Counter(StateManager currentContext, StateFactory factory)
    : base(currentContext, factory) { }
    private bool attacked;
    private bool counterPossible;
 
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
            if (counterPossible)
            {
                if (enemy != null)
                {
                    context.playerCombat.Attack(enemy);
                    Debug.Log("Counter Successful!");
                    SwitchState(factory.Idleing());
                    attacked = true;
                }
                else
                {
                    Debug.LogError("Counter not Successful!");
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

    public void setCounterPossible()
    {
        counterPossible = true;
    }
    public void setCounterNotPossible()
    {
        counterPossible = false;
    }
}
