using Unity.VisualScripting;
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
    const string playerDamage = "playerDamage";
    const string playerAirCounter = "playerAirCounter";
    const string playerDeath = "playerDeath";
    const string playerLastFrame = "playerLastFrame";

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
        if (!stateManager.damageAnim && !stateManager.deathAnim && !stateManager.deathLastFrame)
        {
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
        
        else if(stateManager.damageAnim)
        {
            AnimStateTransitionString(playerDamage);
            PlaySound("MCTakesDamage");

        }
        else if (stateManager.deathAnim)
        {
            AnimStateTransitionString(playerDeath);
            PlaySound("MCDeath");

        }
        else if (stateManager.deathLastFrame)
        {
            AnimStateTransitionString(playerLastFrame);
        }

    }

    void HandleAirborneAnimations(Airborne airborneState)
    {
        var currentSubState = airborneState.GetCurrentSubState();
        if (!stateManager.damageAnim && !stateManager.deathAnim && !stateManager.deathLastFrame)
        {
            if (currentSubState is Jump)
            {
                AnimStateTransitionString(playerJump);
               
            }
            else if (currentSubState is AirCounter)
            {
                AnimStateTransitionString(playerAirCounter);
                PlaySound("MCCounter");
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
        else if (stateManager.damageAnim)
        {
            AnimStateTransitionString(playerDamage);
            PlaySound("MCTakesDamage");

        }
        else if (stateManager.deathAnim)
        {
            AnimStateTransitionString(playerDeath);
            PlaySound("MCDeath");

        }
        else if (stateManager.deathLastFrame)
        {
            AnimStateTransitionString(playerLastFrame);
        }

    }

    public void AnimStateTransitionString(string stateNew)
    {
        if (currentState == stateNew) return;

        animator.Play(stateNew);
        currentState = stateNew;
    }
    public void PlaySound(string soundName)
    {
        if (!SoundManager.Instance.IsSoundPlaying())
        {
            SoundManager.Instance.StopPlayerSound2D();
            SoundManager.Instance.PlayPlayerSound(soundName);
        }
    }



}


