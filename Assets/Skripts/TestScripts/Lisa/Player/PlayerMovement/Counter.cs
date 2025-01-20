
using UnityEngine;

public class Counter : BaseState
{
    public Counter(StateManager currentContext, StateFactory factory)
    : base(currentContext, factory) { }
    private bool attacked;
    StationaryEnemy enemy;
    override public void EnterState()
    {
        attacked = false;
        enemy = new StationaryEnemy();
    }


    override public void UpdateState()
    {
        if (!attacked && context.CheckForEnemy())
        {
            if (enemy.GetCounterPossible())
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    context.playerCombat.Attack(context.currentEnemy);
                    attacked = true;
                    Debug.Log("Counter Successful!");
                }
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
