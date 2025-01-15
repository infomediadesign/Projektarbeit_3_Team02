
using UnityEngine;

public class Counter : BaseState
{
    public Counter(StateManager currentContext, StateFactory factory)
    : base(currentContext, factory) { }
    private bool attacked;
    override public void EnterState()
    {
        attacked = false;
    }


    override public void UpdateState()
    {
        Debug.Log("Counter");

        if(!attacked && context.CheckForEnemy())
        {
            context.playerCombat.Attack(context.currentEnemy);
            attacked = true;
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
