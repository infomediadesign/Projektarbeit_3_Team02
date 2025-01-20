using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CounterUI : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset;   // Position des UIs relativ zum Gegner
    private CanvasGroup canvasGroup;
    public Image timerImage;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
        }
    }

    public void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void UpdateTimer(float time, float maxTime)
    {
        if (timerImage != null)
        {
            timerImage.fillAmount = 1f;
        }
    }

    public void ResetTimer()
    {
        if (timerImage != null)
        {
            timerImage.fillAmount = 1f; 
        }
    }
   
}


