using UnityEngine;
using TMPro;

public class MemoryUIText : MonoBehaviour
{
   public TextMeshProUGUI memoryText;

    private int memoryCount = 0;

    public void IncrementMemoryCount(int amount)
    {
        memoryCount += amount; // Increment memory count
        memoryText.text = memoryCount.ToString(); // Update UI
    }
}
