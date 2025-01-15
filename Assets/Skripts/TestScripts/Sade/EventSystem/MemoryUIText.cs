using UnityEngine;
using TMPro;

public class MemoryUIText : MonoBehaviour
{
    private int memoryCount;

    public void IncrementMemoryCount()
    {
        if (this != null)  // Checking if the component is still valid
        {
            memoryCount++;
            GetComponent<TextMeshProUGUI>().text = $"Memories: {memoryCount}";
        }
        else
            {
                Debug.LogWarning("LifeUIText component has been destroyed.");
            }
        
    }
}
