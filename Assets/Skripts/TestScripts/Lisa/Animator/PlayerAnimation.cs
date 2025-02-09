using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    StateManager stateManager;
    private string currentState;

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
        if (stateManager.currentState is Grounded groundedState)
        {
            HandleGroundedAnimations(groundedState);
        }

        else if (stateManager.currentState is Airborne airborneState)
        {
            HandleAirborneAnimations(airborneState);
        }
    }

    void HandleGroundedAnimations(Grounded groundedState)
    {
        var currentSubState = groundedState.GetCurrentSubState();

        if (currentSubState is Walk)
        {
            AnimStateTransition(playerWalk);
        }
        else if (currentSubState is Idle)
        {
            AnimStateTransition(playerIdle);
        }
       /* else if (currentSubState is Roll)
        {
            AnimStateTransition(playerRoll);
        }
        else if (currentSubState is Block)
        {
            AnimStateTransition(playerBlock);
        }
        else if (currentSubState is Counter)
        {
            AnimStateTransition(playerCounter);
        }*/
    }

    void HandleAirborneAnimations(Airborne airborneState)
    {
        var currentSubState = airborneState.GetCurrentSubState();

       /* if (currentSubState is Jump)
        {
            AnimStateTransition(playerJump);
        }
        else if (currentSubState is AirCounter)
        {
            AnimStateTransition(playerJump);
        }
        else if (currentSubState is Falling)
        {
            AnimStateTransition(playerJump);
        }
        else if (currentSubState is Block)
        {
            AnimStateTransition(playerBlock);
        }*/
    }

    public void AnimStateTransition(string stateNew)
    {
        if (currentState == stateNew) return;

        animator.Play(stateNew);
        currentState = stateNew;
    }
}

