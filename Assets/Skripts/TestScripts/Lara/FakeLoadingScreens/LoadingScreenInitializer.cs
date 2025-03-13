using UnityEngine;

public class LoadingScreenInitializer : MonoBehaviour
{
    // Dieses Skript sollte an das GameObject mit dem EventManager angeh�ngt werden

    void Awake()
    {
        // Sicherstellen, dass dieses GameObject nicht zerst�rt wird
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Wir m�ssen nur sicherstellen, dass das Skript ausgef�hrt wird,
        // um EventManager.Instance zu initialisieren, bevor es genutzt wird
        Debug.Log("LoadingScreenInitializer: Events wurden initialisiert");
    }
}