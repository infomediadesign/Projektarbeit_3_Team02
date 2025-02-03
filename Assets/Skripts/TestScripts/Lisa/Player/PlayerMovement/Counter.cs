
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
        if (context.CheckForEnemy())
        {
            enemy = context.currentEnemy; //gegner reference
        }
        if (enemy == null)
        {
            Debug.LogError("Counter to early/late!");
        }
        
    }


    override public void UpdateState()
    {
        if (!attacked && context.CheckForEnemy())
        {
            if (enemy.GetCounterPossible())
            {
                context.playerCombat.Attack(context.currentEnemy);
                
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
        else
        {
            SwitchState(factory.Idleing());
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
