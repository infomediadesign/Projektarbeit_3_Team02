using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    StateManager stateManager;
    private string currentState;
    private enum AnimationPhase { Start, Loop, End }
    private AnimationPhase currentAnimationPhase = AnimationPhase.Start;

    [SerializeField] private AnimationClip rollStart;
    [SerializeField] private AnimationClip rollLoop;
    [SerializeField] private AnimationClip rollEnd;

    [SerializeField] private AnimationClip blockStart;
    [SerializeField] private AnimationClip blockLoop;
    [SerializeField] private AnimationClip blockEnd;

    [SerializeField] private AnimationClip jumpStart;
    [SerializeField] private AnimationClip jumpLoop;
    [SerializeField] private AnimationClip jumpEnd;

    const string playerIdle = "playerIdle";
    const string playerWalk = "playerWalk";
    const string playerFalling = "playerFall";
    const string playerCounter = "playerCounter";

    private AnimationPhases playerRoll;
    private AnimationPhases playerBlock;
    private AnimationPhases playerJump;



    void Start()
    {
        animator = GetComponent<Animator>();
        stateManager = GetComponent<StateManager>();

        playerBlock = new AnimationPhases(blockStart, blockLoop, blockEnd);
        playerRoll = new AnimationPhases(rollStart, rollLoop, rollEnd);
        playerJump = new AnimationPhases(jumpStart, jumpLoop, jumpEnd);
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
            AnimStateTransition(playerRoll);
        }
        else if (currentSubState is Block)
        {
            AnimStateTransition(playerBlock);
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
            AnimStateTransition(playerJump);
        }
        else if (currentSubState is AirCounter)
        {
            AnimStateTransition(playerJump);
        }
        else if (currentSubState is Falling)
        {
            AnimStateTransitionString(playerFalling);
        }
        else if (currentSubState is Block)
        {
            AnimStateTransition(playerBlock);
        }
    }

    public void AnimStateTransitionString(string stateNew)
    {
        if (currentState == stateNew) return;

        animator.Play(stateNew);
        currentState = stateNew;
    }
    void AnimStateTransition(AnimationPhases animPhases)
    {
        switch (currentAnimationPhase)
        {
            case AnimationPhase.Start:
                animator.Play(animPhases.startAnim.name);
                currentAnimationPhase = AnimationPhase.Loop;
                break;

            case AnimationPhase.Loop:
                animator.Play(animPhases.loopAnim.name);
                break;

            case AnimationPhase.End:
                animator.Play(animPhases.endAnim.name);
                currentAnimationPhase = AnimationPhase.Start;
                break;
        }
    }
}


