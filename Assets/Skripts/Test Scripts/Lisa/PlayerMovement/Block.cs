
using UnityEngine;

public class Block : BaseState
{
    override public void EnterState(StateManager state)
    {
        Debug.Log("entering Block state");
    }


    override public void UpdateState(StateManager state)
    {
        Debug.Log("block");

        if (Input.GetKeyUp(KeyCode.B))
        {
            state.TransitionState(state.idleState);
        }
    }

    override public void ExitState(StateManager state)
    {
        Debug.Log("exiting state");
    }
}

