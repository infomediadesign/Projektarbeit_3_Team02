using UnityEngine;

public class SpriteDisplay : MonoBehaviour
{
    [Header("Trigger Settings")]
    [SerializeField] private float triggerRadius = 2f;

    [Header("Sprite Settings")]
    [SerializeField] private Vector2 spriteOffset = new Vector2(0, 1f); // Offset in Weltkoordinaten

    private bool isPlayerInRange = false;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private GameObject spriteObject;

    private void Start()
    {
        // Find the player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get or create sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Kein SpriteRenderer gefunden! Bitte füge einen SpriteRenderer zu diesem GameObject hinzu.");
            return;
        }

        // Initially hide the sprite
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        if (player == null || spriteRenderer == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= triggerRadius)
        {
            if (!isPlayerInRange)
            {
                isPlayerInRange = true;
                spriteRenderer.enabled = true;
            }
        }
        else
        {
            if (isPlayerInRange)
            {
                isPlayerInRange = false;
                spriteRenderer.enabled = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Only draw gizmos if we're not in play mode
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, triggerRadius);
        }
    }
}