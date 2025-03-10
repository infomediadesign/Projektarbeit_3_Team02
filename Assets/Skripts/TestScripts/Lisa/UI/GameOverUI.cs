using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    public static bool gameOver;
    public static string lastTrackPlayed;
    private const int GAME_OVER_SCENE_INDEX = 5;
    private const int FIRST_LEVEL_SCENE_INDEX = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

      

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnMenuPress()
    {

        Initializer.DestroyInitializer();
        Initializer.Inititalize();
        
        SceneManager.LoadScene("MainMenu");
    }
    public void OnResumePress()
    {
        Debug.Log("[GameOverUI] OnResumePress wurde aufgerufen!");
        lastTrackPlayed = MusicManager.Instance.GetLastTrackPlayed();
        Initializer.DestroyInitializer();
        Initializer.Inititalize();
        // Überprüfe, ob der CheckpointManager existiert
        if (CheckpointManager.Instance != null)
        {
            Debug.Log("[GameOverUI] CheckpointManager gefunden");

            if (CheckpointManager.Instance.HasCheckpoint())
            {
                Debug.Log("[GameOverUI] Letzter Checkpoint gefunden bei Position: " +
                    CheckpointManager.Instance.GetLastCheckpointPosition());

                // Zuerst respawnen, was die Zielszene lädt
                CheckpointManager.Instance.RespawnPlayer();

                // Dann die Game-Over-Szene entladen (asynchron)
                StartCoroutine(UnloadGameOverScene());
            }
            else
            {
                Debug.Log("[GameOverUI] Kein Checkpoint gefunden. Lade Level 1.");

                // Level 1 laden
                SceneManager.LoadScene(FIRST_LEVEL_SCENE_INDEX);

                // Dann die Game-Over-Szene entladen (asynchron)
                StartCoroutine(UnloadGameOverScene());
            }
        }
        else
        {
            Debug.LogWarning("[GameOverUI] CheckpointManager nicht gefunden! Stelle sicher, dass er im Persistent Data Prefab vorhanden ist.");

            // Versuche herauszufinden, welche Szene geladen werden sollte
            Debug.Log("[GameOverUI] Aktuelle Szene: " + SceneManager.GetActiveScene().name +
                " (Index: " + SceneManager.GetActiveScene().buildIndex + ")");

            // Fallback: Lade Level 1, falls kein CheckpointManager gefunden wurde
            Debug.Log("[GameOverUI] Kein CheckpointManager gefunden. Lade Level 1.");
            SceneManager.LoadScene(FIRST_LEVEL_SCENE_INDEX);
        }
    }

    private IEnumerator UnloadGameOverScene()
    {
        // Kurz warten, um sicherzustellen, dass die Zielszene geladen ist
        yield return new WaitForSeconds(0.1f);

        // GameOver-Szene entladen
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(GAME_OVER_SCENE_INDEX);

        yield return unloadOperation;

        Debug.Log("[GameOverUI] GameOver-Szene erfolgreich entladen");
    }
}