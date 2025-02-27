using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Sprite activeSprite;
    public RuntimeAnimatorController inactiveAnimation;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isActivated = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Set default to inactive animation
        if (animator && inactiveAnimation)
        {
            animator.runtimeAnimatorController = inactiveAnimation;
            animator.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            ActivateCheckpoint();
        }
    }

    private void ActivateCheckpoint()
    {
        isActivated = true;

        // Update the checkpoint in the game manager
        CheckpointManager.Instance.SetLastCheckpoint(transform.position);

        // Change visual appearance - stop animation and show static sprite
        if (animator)
        {
            animator.enabled = false;
        }

        if (spriteRenderer && activeSprite)
        {
            spriteRenderer.sprite = activeSprite;
        }
    }
}