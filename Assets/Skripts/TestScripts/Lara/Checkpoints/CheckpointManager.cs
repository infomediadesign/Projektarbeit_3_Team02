using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CheckpointManager : MonoBehaviour
{
    // Singleton instance
    public static CheckpointManager Instance { get; private set; }

    // Player respawn settings
    [SerializeField] private int respawnLives = 4;

    // Last checkpoint position
    private Vector3 lastCheckpointPosition;
    private string lastCheckpointScene;
    private bool hasCheckpoint = false;

    // Memory count at last checkpoint
    private int lastCheckpointMemoryCount = 0;

    // Event-Name f�r die Checkpoint-Aktivierung
    private const string CHECKPOINT_ACTIVATED_EVENT = "OnCheckpointActivated";

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Listen for scene changes to reset checkpoint if needed
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Event-Listener f�r Checkpoint-Aktivierung registrieren
        if (EventManager.Instance != null)
        {
            EventManager.Instance.StartListening<Vector3>(CHECKPOINT_ACTIVATED_EVENT, OnCheckpointActivated);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // Event-Listener beim Zerst�ren des Objekts entfernen
        if (EventManager.Instance != null)
        {
            EventManager.Instance.StopListening<Vector3>(CHECKPOINT_ACTIVATED_EVENT, OnCheckpointActivated);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Kritische �nderung: Nicht den Checkpoint zur�cksetzen beim Szenenwechsel,
        // au�er wenn wir zum Hauptmen� zur�ckkehren
        if (scene.name == "MainMenu")
        {
            Debug.Log("[CheckpointManager] Zum Hauptmen� zur�ckgekehrt. Setze Checkpoint-Daten zur�ck.");
            hasCheckpoint = false;
            lastCheckpointMemoryCount = 0;
        }
        else if (scene.name == "GameOver")
        {
            // Im Game-Over-Bildschirm keine �nderungen an den Checkpoint-Daten vornehmen
            Debug.Log("[CheckpointManager] Game-Over-Szene geladen. Checkpoint-Daten bleiben erhalten.");
        }
        else
        {
            // Bei allen anderen Szenen die Checkpoint-Daten beibehalten
            Debug.Log($"[CheckpointManager] Szene {scene.name} geladen. Checkpoint-Daten bleiben erhalten: " +
                     $"HasCheckpoint={hasCheckpoint}, Position={lastCheckpointPosition}, Szene={lastCheckpointScene}, " +
                     $"Erinnerungen={lastCheckpointMemoryCount}");
        }
    }

    // Event-Handler-Methode f�r die Checkpoint-Aktivierung
    private void OnCheckpointActivated(Vector3 checkpointPosition)
    {
        Debug.Log($"[CheckpointManager] Event-Handler: Checkpoint bei Position {checkpointPosition} wurde aktiviert");
        SetLastCheckpoint(checkpointPosition);
    }

    public void SetLastCheckpoint(Vector3 position)
    {
        Debug.Log($"[CheckpointManager] SetLastCheckpoint aufgerufen mit Position {position}");

        lastCheckpointPosition = position;
        lastCheckpointScene = SceneManager.GetActiveScene().name;
        hasCheckpoint = true;

        // Speichere die aktuelle Anzahl an Erinnerungen
        SaveCurrentMemoryCount();

        Debug.Log($"[CheckpointManager] Checkpoint-Daten aktualisiert: HasCheckpoint={hasCheckpoint}, " +
                 $"Position={lastCheckpointPosition}, Szene={lastCheckpointScene}, " +
                 $"Erinnerungen={lastCheckpointMemoryCount}");
    }

    // Neue Methode zum Speichern der aktuellen Erinnerungen
    private void SaveCurrentMemoryCount()
    {
        // Finde das UI-Objekt f�r die Erinnerungen
        MemoryUIText memoryUI = FindMemoryUIText();
        if (memoryUI != null)
        {
            // Speichere den aktuellen Stand der Erinnerungen
            lastCheckpointMemoryCount = memoryUI.memoryCount;
            Debug.Log($"[CheckpointManager] Erinnerungen zum Zeitpunkt des Checkpoints gespeichert: {lastCheckpointMemoryCount}");
        }
        else
        {
            Debug.LogWarning("[CheckpointManager] MemoryUIText konnte nicht gefunden werden!");
        }
    }

    // Hilfsmethode zum Finden des MemoryUIText-Objekts
    private MemoryUIText FindMemoryUIText()
    {
        // Du k�nntest hier verschiedene Strategien verwenden, je nach Struktur deines Spiels
        // 1. �ber Tag
        GameObject uiObject = GameObject.FindGameObjectWithTag("MemoryUI");
        if (uiObject) return uiObject.GetComponent<MemoryUIText>();

        // 2. Direkt �ber Typ
        MemoryUIText[] memoryUIs = FindObjectsOfType<MemoryUIText>();
        if (memoryUIs.Length > 0) return memoryUIs[0];

        return null;
    }

    public void RespawnPlayer()
    {
        Debug.Log($"[CheckpointManager] RespawnPlayer aufgerufen. Aktueller Status: HasCheckpoint={hasCheckpoint}, " +
                 $"Position={lastCheckpointPosition}, Szene={lastCheckpointScene}, " +
                 $"Erinnerungen={lastCheckpointMemoryCount}");

        if (hasCheckpoint)
        {
            // Load the scene where the checkpoint was activated
            Debug.Log($"[CheckpointManager] Lade Szene {lastCheckpointScene} und positioniere Spieler an Checkpoint {lastCheckpointPosition}");
            SceneManager.LoadScene(lastCheckpointScene);
            // We need to wait for the scene to load before positioning the player
            StartCoroutine(PositionPlayerAfterLoad());
        }
        else
        {
            // If no checkpoint was activated, load level 1 (index 1)
            Debug.Log("[CheckpointManager] Kein Checkpoint aktiviert. Lade Level 1 (Build Index 1)");
            SceneManager.LoadScene(1);

            // Player will be at the default spawn position in the level
            Debug.Log("[CheckpointManager] Spieler wird am urspr�nglichen Spawn-Punkt erscheinen");
        }
    }

    private IEnumerator PositionPlayerAfterLoad()
    {
        // Wait for the scene to load
        yield return null;

        // Find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            // Move player to checkpoint position
            player.transform.position = lastCheckpointPosition;
            Debug.Log($"[CheckpointManager] Spieler an Checkpoint-Position {lastCheckpointPosition} platziert");

            // Reset player health
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.Heal(respawnLives);
            }

            // Stelle die Anzahl der Erinnerungen wieder her
            RestoreMemoryCount();
        }
    }

    // Neue Methode zum Wiederherstellen der Erinnerungen
    private void RestoreMemoryCount()
    {
        MemoryUIText memoryUI = FindMemoryUIText();
        if (memoryUI != null)
        {
            // Setze den Memory Count auf 0
            memoryUI.IncrementMemoryCount(-memoryUI.memoryCount); // Zur�cksetzen auf 0

            // Stelle den gespeicherten Wert wieder her
            memoryUI.IncrementMemoryCount(lastCheckpointMemoryCount);

            Debug.Log($"[CheckpointManager] Erinnerungen auf {lastCheckpointMemoryCount} zur�ckgesetzt");
        }
        else
        {
            Debug.LogWarning("[CheckpointManager] MemoryUIText konnte nach dem Laden der Szene nicht gefunden werden!");
        }
    }

    // Optional: Getter-Methode f�r die letzte Checkpoint-Position
    public Vector3 GetLastCheckpointPosition()
    {
        return lastCheckpointPosition;
    }

    // Optional: Getter-Methode f�r den Status des Checkpoints
    public bool HasCheckpoint()
    {
        return hasCheckpoint;
    }

    // Optional: Getter-Methode f�r die gespeicherte Anzahl an Erinnerungen
    public int GetMemoryCount()
    {
        return lastCheckpointMemoryCount;
    }
}