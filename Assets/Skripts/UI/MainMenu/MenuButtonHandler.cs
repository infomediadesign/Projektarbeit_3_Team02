using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuButtonHandler : MonoBehaviour
{
     public Slider slider; // Drag your slider here
    public TMP_Text valueText; // Drag your TextMeshPro object here

    void Start()
    {
        // Initialize the TextMeshPro with the default slider value
        UpdateValueText(slider.value);

        // Add listener for when the slider value changes
        slider.onValueChanged.AddListener(UpdateValueText);
    }

    void UpdateValueText(float value)
    {
        // Update the TextMeshPro text to show the current slider value
        valueText.text = value.ToString("0"); // Round to whole numbers
    }

    void OnDestroy()
    {
        // Clean up the listener to avoid memory leaks
        slider.onValueChanged.RemoveListener(UpdateValueText);
    }
    /*public Text valueText;
    public void OnSliderChanged(float value)
    {
        valueText.text = value.ToString();
    }*/
}
