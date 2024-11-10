
using UnityEngine;

public class AirCounter : BaseState
{
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering airCounter state");
    }


    override public void UpdateState(StateManager state)
    {
        Debug.Log("airCounter");

        state.TransitionState(state.idleState);
        
    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
    }
}
