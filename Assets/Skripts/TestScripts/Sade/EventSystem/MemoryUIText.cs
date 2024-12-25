using UnityEngine;
using TMPro;

public class MemoryUIText : MonoBehaviour
{
    int memoryCount;

    public void IncrementMemoryCount()
    {
        memoryCount++;
        GetComponent<TextMeshProUGUI>().text = $"Memories: {memoryCount}";
    }
}
