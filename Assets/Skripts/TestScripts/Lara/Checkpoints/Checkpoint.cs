using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Sprite activeSprite;
    public RuntimeAnimatorController inactiveAnimation;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isActivated = false;

    // Event-Name für die Checkpoint-Aktivierung
    private const string CHECKPOINT_ACTIVATED_EVENT = "OnCheckpointActivated";

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
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[Checkpoint] Player kam an Checkpoint bei Position {transform.position} vorbei");

            if (!isActivated)
            {
                ActivateCheckpoint();
            }
        }
    }

    private void ActivateCheckpoint()
    {
        isActivated = true;
        Debug.Log($"[Checkpoint] Checkpoint bei Position {transform.position} wurde aktiviert");

        // Direkte Aktualisierung über den CheckpointManager
        CheckpointManager.Instance.SetLastCheckpoint(transform.position);

        // Event auslösen mit der Position als Parameter
        if (EventManager.Instance != null)
        {
            EventManager.Instance.TriggerEvent(CHECKPOINT_ACTIVATED_EVENT, transform.position);
            Debug.Log($"[Checkpoint] Event '{CHECKPOINT_ACTIVATED_EVENT}' mit Position {transform.position} wurde ausgelöst");
        }

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