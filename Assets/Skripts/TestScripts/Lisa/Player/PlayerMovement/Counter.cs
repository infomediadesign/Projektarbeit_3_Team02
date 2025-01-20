
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
            Debug.LogError("no stationary enemy!");
        }
        else
        {
            Debug.Log("Enemy found");
        }
    }


    override public void UpdateState()
    {
        if (!attacked && context.CheckForEnemy())
        {
            if (enemy.GetCounterPossible())
            {
                Debug.Log("Counter is possible now!");
                context.playerCombat.Attack(context.currentEnemy);
                
                Debug.Log("Counter Successful!");
                SwitchState(factory.Idleing());
                attacked = true;
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
        Debug.Log("exiting state");
    }
}
