using UnityEngine;

public class LoadingScreenManager : MonoBehaviour
{
    private GameObject[] loadingScreens;

    private void Awake()
    {
        // Alle Child-Objekte finden und deaktivieren
        int childCount = transform.childCount;
        loadingScreens = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            loadingScreens[i] = transform.GetChild(i).gameObject;
            loadingScreens[i].SetActive(false);
        }
    }

    private void Start()
    {
        // Event-Listener registrieren
        EventManager.Instance.StartListening<string>("ShowLoadingScreen", ShowLoadingScreen);
    }

    private void OnDestroy()
    {
        // Event-Listener entfernen
        if (EventManager.Instance != null)
        {
            EventManager.Instance.StopListening<string>("ShowLoadingScreen", ShowLoadingScreen);
        }
    }

    private void ShowLoadingScreen(string screenName)
    {
        // Zuerst alle Loading-Screens deaktivieren
        foreach (GameObject screen in loadingScreens)
        {
            screen.SetActive(false);
        }

        // Den angegebenen Loading-Screen finden und aktivieren
        bool foundScreen = false;
        foreach (GameObject screen in loadingScreens)
        {
            if (screen.name == screenName)
            {
                screen.SetActive(true);
                foundScreen = true;
                break;
            }
        }

        if (!foundScreen)
        {
            Debug.LogWarning($"Loading screen with name '{screenName}' not found!");
        }
    }
}