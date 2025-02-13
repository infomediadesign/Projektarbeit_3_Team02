using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    StateManager stateManager;
    private string currentState;

    const string playerIdle = "playerIdle";
    const string playerWalk = "playerWalk";
    const string playerFalling = "playerFall";
    const string playerCounter = "playerCounter";
    const string playerRoll = "playerRoll";
    const string playerJump = "playerJump";
    const string playerBlock = "playerBlock";

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
            AnimStateTransitionString(playerWalk);

        }
        else if (currentSubState is Idle)
        {
            AnimStateTransitionString(playerIdle);
        }
        else if (currentSubState is Roll)
        {
            AnimStateTransitionString(playerRoll);
        }
        else if (currentSubState is Block)
        {
            AnimStateTransitionString(playerBlock);
        }
        else if (currentSubState is Counter)
        {
            AnimStateTransitionString(playerCounter);
        }
    }

    void HandleAirborneAnimations(Airborne airborneState)
    {
        var currentSubState = airborneState.GetCurrentSubState();

        if (currentSubState is Jump)
        {
            AnimStateTransitionString(playerJump);
        }
        else if (currentSubState is AirCounter)
        {
            AnimStateTransitionString(playerJump);
        }
        else if (currentSubState is Falling)
        {
            AnimStateTransitionString(playerFalling);
        }
        else if (currentSubState is Block)
        {
            AnimStateTransitionString(playerBlock);
        }
    }

    public void AnimStateTransitionString(string stateNew)
    {
        if (currentState == stateNew) return;

        animator.Play(stateNew);
        currentState = stateNew;
    }

  


}


