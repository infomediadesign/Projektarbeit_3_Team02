using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private DialogData dialogData;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private Color rangeGizmoColor = new Color(0, 1, 0, 0.2f);

    private int currentPartIndex = 0;
    private bool isInRange = false;

    private void Start()
    {
        // Dialog-Panel initial ausblenden
        if (dialogPanel == null || dialogText == null || dialogData == null)
        {
            Debug.LogError("Missing references on " + gameObject.name);
            return;
        }
        dialogPanel.SetActive(false);

        // Finde und setze die Kamera
        Canvas dialogCanvas = GameObject.Find("DialogCanvas").GetComponent<Canvas>();
        Camera mainCam = Camera.main;
        if (dialogCanvas && mainCam)
        {
            dialogCanvas.worldCamera = mainCam;
        }
    }

    private void Update()
    {
        // Prüfe Position des Players
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < interactionRange && !isInRange)
            {
                OnPlayerEnterRange();
            }
            else if (distance >= interactionRange && isInRange)
            {
                OnPlayerExitRange();
            }
        }

        // Überprüfe Mausklick nur wenn in Range und mehrere Textteile vorhanden
        if (isInRange && dialogData.hasMultipleParts && Input.GetMouseButtonDown(0))
        {
            ShowNextPart();
        }
    }

    private void ShowNextPart()
    {
        currentPartIndex++;
        if (currentPartIndex >= dialogData.dialogParts.Length)
        {
            currentPartIndex = 0;  // Zurück zum Anfang, wenn alle Teile gezeigt wurden
        }
        dialogText.text = dialogData.dialogParts[currentPartIndex];
    }

    private void OnPlayerEnterRange()
    {
        isInRange = true;
        currentPartIndex = 0;  // Reset auf ersten Teil
        dialogPanel.SetActive(true);
        dialogText.text = dialogData.dialogParts[currentPartIndex];
    }

    private void OnPlayerExitRange()
    {
        isInRange = false;
        dialogPanel.SetActive(false);
    }

    private void OnValidate()
    {
        if (dialogData == null)
        {
            Debug.LogWarning("Kein DialogData zugewiesen für " + gameObject.name);
        }
    }

    // Zeigt den Interaktionsbereich NUR im Editor und nur wenn ausgewählt an
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = rangeGizmoColor;
        Gizmos.DrawWireSphere(transform.position, interactionRange);

        // Fülle den Kreis mit halbtransparenter Farbe
        Gizmos.color = new Color(rangeGizmoColor.r, rangeGizmoColor.g, rangeGizmoColor.b, 0.1f);
        Gizmos.DrawSphere(transform.position, interactionRange);
    }
}