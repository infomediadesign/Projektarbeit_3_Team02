using UnityEngine;
public class GrabDisplay : MonoBehaviour
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    private SpriteRenderer spriteRenderer;
    private bool isActivated = false;
    // Event-Name für die Checkpoint-Aktivierung
    private const string GRAVE_ACTIVATED_EVENT = "OnCheckpointActivated";

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set default to inactive sprite
        if (spriteRenderer && inactiveSprite)
        {
            spriteRenderer.sprite = inactiveSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[Checkpoint] Player kam an Grab bei Position {transform.position} vorbei");
            if (!isActivated)
            {
                ActivateGrave();
            }
        }
    }

    private void ActivateGrave()
    {
        isActivated = true;
        Debug.Log($"[Grab] Grab bei Position {transform.position} wurde aktiviert");

        // Direkte Aktualisierung über den CheckpointManager
        //CheckpointManager.Instance.SetLastCheckpoint(transform.position);

        // Event auslösen mit der Position als Parameter
        /*
        if (EventManager.Instance != null)
        {
            EventManager.Instance.TriggerEvent(GRAVE_ACTIVATED_EVENT, transform.position);
            Debug.Log($"[Checkpoint] Event '{GRAVE_ACTIVATED_EVENT}' mit Position {transform.position} wurde ausgelöst");
        }*/

        // Change visual appearance to active sprite
        if (spriteRenderer && activeSprite)
        {
            spriteRenderer.sprite = activeSprite;
        }
        //SoundManager.Instance.PlaySound2D("WoodenFigureFound");
    }
}