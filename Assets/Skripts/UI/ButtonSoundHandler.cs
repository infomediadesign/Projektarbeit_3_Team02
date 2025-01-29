using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSoundHandler : MonoBehaviour
{
    [SerializeField] private string buttonSoundEvent = "ButtonClicked"; //default event for button click sound

    private void Start()
    {
        //get the button component and add listener
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() =>
            {
                //trigger the button sound event
                EventManager.Instance.TriggerEvent(buttonSoundEvent);
            });
        }
        else
        {
            Debug.LogWarning("ButtonSound script is missing a Button component.");
        }
    }
}
