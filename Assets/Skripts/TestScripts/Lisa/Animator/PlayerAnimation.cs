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
        if (stateManager.currentState == stateManager.walkState)
        {          
            AnimStateTransition(playerWalk);
        }
        else if (stateManager.currentState == stateManager.idleState)
        {
            AnimStateTransition(playerIdle);
        }
        else if (stateManager.currentState == stateManager.rollState)
        {
            AnimStateTransition(playerRoll);
        }
        else if (stateManager.currentState == stateManager.jumpState)
        {
            AnimStateTransition(playerJump);
        }
        else if (stateManager.currentState == stateManager.blockState || stateManager.currentState == stateManager.jumpBlockState)
        {
            AnimStateTransition(playerBlock);
        }
        else if (stateManager.currentState == stateManager.airCounterState)
        {
            AnimStateTransition(playerAirCounter);
        }
        else if (stateManager.currentState == stateManager.counterState)
        {
            AnimStateTransition(playerCounter);
        }
        else if (stateManager.currentState == stateManager.fallingState) 
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
