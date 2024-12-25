using UnityEngine;
using TMPro;

public class LifeUIText : MonoBehaviour
{
    int lifeCount;

    public void IncrementLifeCount()
    {
        lifeCount++;
        GetComponent<TextMeshProUGUI>().text = $"Lives: {lifeCount}";
    }
}
