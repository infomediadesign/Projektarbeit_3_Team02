using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    StateManager stateManager;
    private string currentState;

    //daraus sollte man enum machen

    const string playerIdle = "playerIdle";
    const string playerWalk = "playerWalk";
    const string playerJump = "playerJump";
    const string playerBlock = "playerBlock";
    const string playerCounter = "playerCounter";
    const string playerRoll = "playerRoll";
    const string playerAirCounter = "playerAirCounter";
    const string playerFalling = "playerFalling";
    void Start()
    {
        animator = GetComponent<Animator>();
        stateManager = GetComponent<StateManager>();
   
    }
    void Update()
    {
        if (stateManager.currentState == stateManager.states.Walking())
        {          
            AnimStateTransition(playerWalk);
        }
        else if (stateManager.currentState == stateManager.states.Idleing())
        {
            AnimStateTransition(playerIdle);
        }
        else if (stateManager.currentState == stateManager.states.Rolling())
        {
            AnimStateTransition(playerRoll);
        }
        else if (stateManager.currentState == stateManager.states.Jumping())
        {
            AnimStateTransition(playerJump);
        }
        else if (stateManager.currentState == stateManager.states.Blocking() || stateManager.currentState == stateManager.states.JumpBlocking())
        {
            AnimStateTransition(playerBlock);
        }
        else if (stateManager.currentState == stateManager.states.AirCountering())
        {
            AnimStateTransition(playerAirCounter);
        }
        else if (stateManager.currentState == stateManager.states.Countering())
        {
            AnimStateTransition(playerCounter);
        }
        else if (stateManager.currentState == stateManager.states.Fall()) 
        {
            //AnimStateTransition(playerFalling);
            AnimStateTransition(playerIdle);
        }
    }

    public void AnimStateTransition(string stateNew)
    {
        if (currentState == stateNew) return;

        animator.Play(stateNew);
        currentState = stateNew;
    }
}
