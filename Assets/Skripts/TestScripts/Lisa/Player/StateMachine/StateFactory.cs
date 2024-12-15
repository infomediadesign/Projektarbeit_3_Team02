using UnityEngine;

public class StateFactory
{
   StateManager context;
   public StateFactory(StateManager currentContext)
    {
        context = currentContext;
    }

    /*public Walk walkState = new Walk();
    public Jump jumpState = new Jump();
    public JumpBlock jumpBlockState = new JumpBlock();
    public Roll rollState = new Roll();
    public Counter counterState = new Counter();
    public AirCounter airCounterState = new AirCounter();
    public Block blockState = new Block();
    public Idle idleState = new Idle();
    public Falling fallingState = new Falling();
    public Grounded groundedState = new Grounded();*/

    public BaseState Idleing()
    {
        return new Idle(context, this);
    }
    public BaseState Walking()
    {
        return new Walk(context, this);
    }
    public BaseState Rolling()
    {
        return new Roll(context, this);
    }
    public BaseState Blocking()
    {
        return new Block(context, this);
    }
    public BaseState Jumping()
    {
        return new Jump(context, this);
    }
    public BaseState Fall()
    {
        return new Falling(context, this);
    }
    public BaseState Countering()
    {
        return new Counter(context, this);
    }
    public BaseState AirCountering()
    {
        return new AirCounter(context, this);
    }
    public BaseState JumpBlocking()
    {
        return new JumpBlock(context, this);
    }
    public BaseState Ground()
    {
        return new Grounded(context, this);
    }
}
