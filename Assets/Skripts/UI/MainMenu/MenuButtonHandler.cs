using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonHandler : MonoBehaviour
{
    public Text valueText;
    public void OnSliderChanged(float value)
    {
        valueText.text = value.ToString();
    }
}
