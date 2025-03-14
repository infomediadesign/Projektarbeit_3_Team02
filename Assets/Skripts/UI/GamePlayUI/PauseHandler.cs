using UnityEngine;
using UnityEngine.EventSystems;

public class PauseHandler : MonoBehaviour
{
    public InputSystem_Actions inputActions;
    public InputSystem_Actions.UIActions UIControls;
    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        UIControls = inputActions.UI;
    }
    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.P))
        if (UIControls.PauseGame.triggered)
        {
            if (Time.timeScale == 0)
            {
                EventManager.Instance.TriggerEvent("ResumeGame");
                UIManager.startPressed = true;
            }
            else
            {
                EventManager.Instance.TriggerEvent("PauseGame");
                UIManager.startPressed = false;
            }
          
        }

    }
    public void OnMenuPress()
    {
        EventManager.Instance.TriggerEvent("OnBackToMenuPress");
    }
    private void OnEnable()
    {
        UIControls.Enable();

    }
    private void OnDisable()
    {
        UIControls.Disable();

    }
}
