using UnityEngine;

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
            }
            else
            {
                EventManager.Instance.TriggerEvent("PauseGame");
            }
        }
        //hier noch die anderen Buttons einfügen
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
