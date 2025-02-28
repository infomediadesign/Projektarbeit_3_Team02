using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static bool gameOver;
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
        gameOver = true;
        Debug.Log("game over :" + gameOver);
        SceneManager.LoadScene("MainMenu");
    }
    public void OnResumePress()
    {
        Debug.Log("OnResumePress wurde aufgerufen!");

        // Überprüfe, ob der CheckpointManager existiert
        if (CheckpointManager.Instance != null)
        {
            Debug.Log("CheckpointManager gefunden, versuche zu respawnen...");

            // Verwende den CheckpointManager, um den Spieler zum letzten Checkpoint zurückzubringen
            CheckpointManager.Instance.RespawnPlayer();
        }
        else
        {
            Debug.LogWarning("CheckpointManager nicht gefunden! Stelle sicher, dass er im Persistent Data Prefab vorhanden ist.");

            // Versuche herauszufinden, welche Szene geladen werden sollte
            Debug.Log("Aktuelle Szene: " + SceneManager.GetActiveScene().name + " (Index: " + SceneManager.GetActiveScene().buildIndex + ")");
            Debug.Log("Anzahl der Szenen in den Build-Einstellungen: " + SceneManager.sceneCountInBuildSettings);

            // Fallback: Lade das letzte Level, falls kein CheckpointManager gefunden wurde
            if (SceneManager.sceneCountInBuildSettings > 1)
            {
                int targetScene = SceneManager.GetActiveScene().buildIndex - 1;
                Debug.Log("Versuche, zur Szene mit Index " + targetScene + " zurückzukehren");
                SceneManager.LoadScene(targetScene);
            }
        }
    }

    public void TestButton()
    {
        Debug.Log("TestButton wurde gedrückt!");
        SceneManager.LoadScene("Tech Demo Level 1"); 
    }
}
