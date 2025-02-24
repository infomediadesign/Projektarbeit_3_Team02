using UnityEngine;

public class MenuScreenHandler : MonoBehaviour
{
    public string screenName; 

    public void OnButtonClick()
    {
        EventManager.Instance.TriggerEvent("ShowScreen", screenName);
    }
}
