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
    void Start()
    {
        animator = GetComponent<Animator>();
        stateManager = GetComponent<StateManager>();
   
    }

    // es fehlen noch animator parameter, die in den states oder im state manager gesetzt werden (zb walkleft oder right)
    void Update()
    {
        if (stateManager.currentState == stateManager.walkState)
        {
            AnimStateTransition(playerWalk);
            Debug.Log("walk anim");
        }
        else if (stateManager.currentState == stateManager.idleState)
        {
            AnimStateTransition(playerIdle);
            Debug.Log("idle anim");
        }
        else if (stateManager.currentState == stateManager.rollState)
        {
            AnimStateTransition(playerRoll);
            Debug.Log("roll anim");
        }
        else if (stateManager.currentState == stateManager.jumpState)
        {
            AnimStateTransition(playerJump);
            Debug.Log("jump anim");
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
    }

    public void AnimStateTransition(string stateNew)
    {
        if (currentState == stateNew) return;

        animator.Play(stateNew);
        currentState = stateNew;
    }
}
