
using UnityEngine;

public class Counter : BaseState
{
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering Counter state");
    }


    override public void UpdateState(StateManager state)
    {
        Debug.Log("Counter");

        state.TransitionState(state.idleState);

    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
    }
}
