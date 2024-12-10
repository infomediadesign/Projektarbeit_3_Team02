using UnityEngine;

public class StateFactory
{
    StateManager context;
   public StateFactory(StateManager currentContext)
    {
        context = currentContext;
    }

    public Walk walkState = new Walk();
    public Jump jumpState = new Jump();
    public JumpBlock jumpBlockState = new JumpBlock();
    public Roll rollState = new Roll();
    public Counter counterState = new Counter();
    public AirCounter airCounterState = new AirCounter();
    public Block blockState = new Block();
    public Idle idleState = new Idle();
    public Falling fallingState = new Falling();
    public Grounded groundedState = new Grounded();
}
