
using UnityEngine;

public abstract class BaseState
{
    protected bool isRootState = false;
    protected StateFactory factory;
    protected StateManager context;
    protected BaseState currentSubState;
    protected BaseState currentSuperState;
    public BaseState(StateManager currentContext, StateFactory sFactory)
    {
        context = currentContext;
        factory = sFactory;
    }
    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitializeSubState();

    public void UpdateStates() 
    {
        UpdateState();
        if(currentSubState != null)
        {
            currentSubState.UpdateStates();
        }
    }
    protected void SwitchState(BaseState newState)
    {
        ExitState();
        newState.EnterState();
        if (isRootState)
        {
            context.currentState = newState;
        }
        else if(currentSuperState != null)
        {
            currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(BaseState newSuperState)
    {
        currentSuperState = newSuperState;
    }

    protected void SetSubState(BaseState newSubState)
    {
        currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
