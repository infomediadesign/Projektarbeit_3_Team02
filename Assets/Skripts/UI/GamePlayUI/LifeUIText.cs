using UnityEngine;
using TMPro;

public class LifeUIText : MonoBehaviour
{
    public TextMeshProUGUI lifeText;

    private int lifeCount = 0;

    public void IncrementLifeCount(int amount) // Update this method to accept an int parameter
    {
        lifeCount += amount; // Increment life count
        lifeText.text = lifeCount.ToString(); // Update UI
    }
}
