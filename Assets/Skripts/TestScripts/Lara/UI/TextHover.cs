using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TextHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text textComponent;
    private TMP_Text tmpText;
    private Vector3 originalScale;

    public float scaleFactor = 1.2f;

    void Awake()
    {
        originalScale = transform.localScale; // Urspr�ngliche Gr��e speichern
    }

    void OnEnable()
    {
        transform.localScale = originalScale; // Gr��e zur�cksetzen
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * scaleFactor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}
