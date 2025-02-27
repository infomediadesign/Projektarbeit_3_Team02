using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    // Singleton instance
    public static CheckpointManager Instance { get; private set; }

    // Player respawn settings
    [SerializeField] private int respawnLives = 3;

    // Last checkpoint position
    private Vector3 lastCheckpointPosition;
    private string lastCheckpointScene;
    private bool hasCheckpoint = false;

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
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset checkpoint if we load a different scene (not from GameOver)
        if (scene.name != "GameOver" && scene.name != lastCheckpointScene)
        {
            hasCheckpoint = false;
        }
    }

    public void SetLastCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
        lastCheckpointScene = SceneManager.GetActiveScene().name;
        hasCheckpoint = true;
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

    private System.Collections.IEnumerator PositionPlayerAfterLoad()
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
}