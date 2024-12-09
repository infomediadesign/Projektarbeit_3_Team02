
using UnityEngine;

public abstract class BaseState
{
   // protected StateFactory factory;
    protected StateManager context;
    /*public BaseState(StateManager currentContext, StateFactory sFactory)
    {
        context = currentContext;
        factory = sFactory;
    }*/
    public abstract void EnterState(StateManager state);

    public abstract void UpdateState(StateManager state);

    public abstract void ExitState(StateManager state);
}
