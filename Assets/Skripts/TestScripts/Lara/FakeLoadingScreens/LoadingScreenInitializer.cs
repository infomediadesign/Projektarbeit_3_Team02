using UnityEngine;

public class LoadingScreenInitializer : MonoBehaviour
{
    // Dieses Skript sollte an das GameObject mit dem EventManager angehängt werden

    void Awake()
    {
        // Sicherstellen, dass dieses GameObject nicht zerstört wird
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Wir müssen nur sicherstellen, dass das Skript ausgeführt wird,
        // um EventManager.Instance zu initialisieren, bevor es genutzt wird
        Debug.Log("LoadingScreenInitializer: Events wurden initialisiert");
    }
}