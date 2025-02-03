using UnityEngine;
using TMPro;

public class InstructionTextboxes : MonoBehaviour
{
    [Header("Trigger Settings")]
    [SerializeField] private float triggerRadius = 2f;
    [SerializeField][TextArea] private string displayText = "Text: ";

    [Header("UI Settings")]
    [SerializeField] private GameObject textPrefab; // Assign a TextMeshPro prefab
    [SerializeField] private Vector2 textOffset = new Vector2(0, 50f); // Offset from trigger box position

    private TextMeshProUGUI textDisplay;
    private GameObject textObject;
    private bool isPlayerInRange = false;
    private Transform player;
    private SpriteRenderer boxSprite;
    private Canvas canvas;

    private void Start()
    {
        // Find the player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Find the text canvas
        canvas = GameObject.FindGameObjectWithTag("TextCanvas").GetComponent<Canvas>();

        // Create text object from prefab
        if (textPrefab != null && canvas != null)
        {
            textObject = Instantiate(textPrefab, canvas.transform);
            textDisplay = textObject.GetComponent<TextMeshProUGUI>();
            textDisplay.text = "";

            // Position the text relative to the trigger box
            UpdateTextPosition();
        }

        // Hide the sprite in game mode
        boxSprite = GetComponent<SpriteRenderer>();
        if (boxSprite != null)
        {
            boxSprite.enabled = false;
        }
    }

    private void Update()
    {
        if (player == null || textDisplay == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= triggerRadius)
        {
            if (!isPlayerInRange)
            {
                isPlayerInRange = true;
                textDisplay.text = displayText;
            }
        }
        else
        {
            if (isPlayerInRange)
            {
                isPlayerInRange = false;
                textDisplay.text = "";
            }
        }

        // Update text position if the trigger box moves
        UpdateTextPosition();
    }

    private void UpdateTextPosition()
    {
        if (textObject != null)
        {
            // Convert world position to screen position
            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            textObject.GetComponent<RectTransform>().position = screenPos + textOffset;
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

    private void OnDestroy()
    {
        // Clean up the text object when the trigger box is destroyed
        if (textObject != null)
        {
            Destroy(textObject);
        }
    }
}