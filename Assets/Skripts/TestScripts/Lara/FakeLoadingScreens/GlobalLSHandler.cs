using UnityEngine;

public class GlobalLSHandler : MonoBehaviour
{
    private void Start()
    {
        // Event-Listener registrieren
        EventManager.Instance.StartListening("HideLoadingScreen", HideAllLoadingScreens);
    }

    private void OnDestroy()
    {
        // Event-Listener entfernen
        if (EventManager.Instance != null)
        {
            EventManager.Instance.StopListening("HideLoadingScreen", HideAllLoadingScreens);
        }
    }

    private void HideAllLoadingScreens()
    {
        // Finde das LoadingScreens GameObject
        GameObject loadingScreensParent = transform.gameObject;

        // Deaktiviere alle Child-Objekte
        for (int i = 0; i < loadingScreensParent.transform.childCount; i++)
        {
            loadingScreensParent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}