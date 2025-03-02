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

    // Event-Name für die Checkpoint-Aktivierung
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

        // Event-Listener für Checkpoint-Aktivierung registrieren
        if (EventManager.Instance != null)
        {
            EventManager.Instance.StartListening<Vector3>(CHECKPOINT_ACTIVATED_EVENT, OnCheckpointActivated);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // Event-Listener beim Zerstören des Objekts entfernen
        if (EventManager.Instance != null)
        {
            EventManager.Instance.StopListening<Vector3>(CHECKPOINT_ACTIVATED_EVENT, OnCheckpointActivated);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset checkpoint if we load a different scene (not from GameOver)
        if (scene.name != "GameOver" && scene.name != lastCheckpointScene)
        {
            hasCheckpoint = false;
        }
    }

    // Event-Handler-Methode für die Checkpoint-Aktivierung
    private void OnCheckpointActivated(Vector3 checkpointPosition)
    {
        Debug.Log($"[CheckpointManager] Event-Handler: Checkpoint bei Position {checkpointPosition} wurde aktiviert");
        SetLastCheckpoint(checkpointPosition);
    }

    public void SetLastCheckpoint(Vector3 position)
    {
        Debug.Log($"[CheckpointManager] Position vom letzten Checkpoint wird aktualisiert");
        lastCheckpointPosition = position;
        lastCheckpointScene = SceneManager.GetActiveScene().name;
        hasCheckpoint = true;

        Debug.Log($"[CheckpointManager] Position vom letzten Checkpoint lautet: {position} in Szene {lastCheckpointScene}");
    }

    public void RespawnPlayer()
    {
        if (hasCheckpoint)
        {
            // Load the scene where the checkpoint was activated
            SceneManager.LoadScene(lastCheckpointScene);
            // We need to wait for the scene to load before positioning the player
            StartCoroutine(PositionPlayerAfterLoad());
        }
        else
        {
            // If no checkpoint was activated, restart the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            // Reset player health
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.Heal(respawnLives);
            }
        }
    }

    // Optional: Getter-Methode für die letzte Checkpoint-Position
    public Vector3 GetLastCheckpointPosition()
    {
        return lastCheckpointPosition;
    }

    // Optional: Getter-Methode für den Status des Checkpoints
    public bool HasCheckpoint()
    {
        return hasCheckpoint;
    }
}