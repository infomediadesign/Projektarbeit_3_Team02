using UnityEngine;
using TMPro;

public class LifeUIText : MonoBehaviour
{
    private int lifeCount;

    public void IncrementLifeCount()
    {
        if (this != null)  // Checking if the component is still valid
            {
                lifeCount++;
                GetComponent<TextMeshProUGUI>().text = $"Lives: {lifeCount}";
            }
            else
            {
                Debug.LogWarning("LifeUIText component has been destroyed.");
            }
    }
}
