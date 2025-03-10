using UnityEngine;

public class GrabDisplay : MonoBehaviour
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    private SpriteRenderer spriteRenderer;
    private bool isActivated = false;

    // Event-Name f�r die Checkpoint-Aktivierung
    private const string GRAVE_ACTIVATED_EVENT = "OnGraveActivated";

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
            Debug.Log($"[Grab] Player kam an Grab bei Position {transform.position} vorbei");
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

        // Direkte Aktualisierung �ber den CheckpointManager
        //CheckpointManager.Instance.SetLastCheckpoint(transform.position);

        // Event ausl�sen ohne Parameter (wir verwenden einen einfachen Action-Event)
        if (EventManager.Instance != null)
        {
            EventManager.Instance.TriggerEvent(GRAVE_ACTIVATED_EVENT);
            Debug.Log($"[Grab] Event '{GRAVE_ACTIVATED_EVENT}' wurde ausgel�st");
        }

        // Direkter Aufruf bleibt als Fallback
        if (GrabManager.Instance != null)
        {
            GrabManager.Instance.OnGraveActivated();
            Debug.Log($"[Grab] GrabManager wurde direkt �ber Aktivierung bei Position {transform.position} informiert");
        }

        // Change visual appearance to active sprite
        if (spriteRenderer && activeSprite)
        {
            spriteRenderer.sprite = activeSprite;
        }

        //SoundManager.Instance.PlaySound2D("WoodenFigureFound");
    }
}