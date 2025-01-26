using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
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
    }
}
