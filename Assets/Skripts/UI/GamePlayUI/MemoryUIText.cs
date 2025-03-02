using UnityEngine;
using TMPro;

public class MemoryUIText : MonoBehaviour
{
    public TextMeshProUGUI memoryText;

    // Von private zu public geändert, damit CheckpointManager darauf zugreifen kann
    public int memoryCount = 0;

    private void Start()
    {
        // Initialisiere den Text mit dem aktuellen Wert
        UpdateMemoryText();
    }

    public void IncrementMemoryCount(int amount)
    {
        memoryCount += amount; // Increment or decrement memory count
        UpdateMemoryText(); // Update UI

        Debug.Log($"[MemoryUIText] Erinnerungen aktualisiert: {memoryCount}");
    }

    // Neue Methode zum direkten Setzen des Werts
    public void SetMemoryCount(int value)
    {
        memoryCount = value;
        UpdateMemoryText();

        Debug.Log($"[MemoryUIText] Erinnerungen direkt gesetzt auf: {memoryCount}");
    }

    // Hilfsmethode zum Aktualisieren des Textes
    private void UpdateMemoryText()
    {
        if (memoryText != null)
        {
            memoryText.text = memoryCount.ToString();
        }
    }
}