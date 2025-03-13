using UnityEngine;
using System.Collections;

public class LoadingScreenTimer : MonoBehaviour
{
    [Tooltip("Zeit in Sekunden, die der Loading-Screen angezeigt wird, nachdem er aktiviert wurde")]
    public float displayDuration = 3.0f;

    private Coroutine hideTimerCoroutine;

    private void OnEnable()
    {
        // Wenn das GameObject aktiviert wird, starte den Timer
        if (hideTimerCoroutine != null)
        {
            StopCoroutine(hideTimerCoroutine);
        }

        hideTimerCoroutine = StartCoroutine(HideAfterDelay());
    }

    private void OnDisable()
    {
        // Wenn das GameObject deaktiviert wird, stoppe den Timer
        if (hideTimerCoroutine != null)
        {
            StopCoroutine(hideTimerCoroutine);
            hideTimerCoroutine = null;
        }
    }

    private IEnumerator HideAfterDelay()
    {
        // Warte für die angegebene Zeit
        yield return new WaitForSeconds(displayDuration);

        // Triggere das HideLoadingScreen-Event
        EventManager.Instance.TriggerEvent("HideLoadingScreen");

        // Deaktiviere dieses GameObject
        gameObject.SetActive(false);

        hideTimerCoroutine = null;
    }
}