
using UnityEngine;

public class Counter : BaseState
{
    private bool attacked;
    override public void EnterState(StateManager state)
    {
        attacked = false;
    }


    override public void UpdateState(StateManager state)
    {
        Debug.Log("Counter");

        if(!attacked && state.CheckForEnemy())
        {
            state.playerCombat.Attack(state.currentEnemy);
            attacked = true;
        }
        else
        {
            state.TransitionState(state.idleState);
        }
        

    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
    }
}
