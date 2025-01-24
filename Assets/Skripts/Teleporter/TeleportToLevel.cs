using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TeleportToLevel : MonoBehaviour
{
  
public int targetSceneBuildIndex; // Target scene to load
    public int currentSceneBuildIndex; // Current scene to unload

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(TeleportPlayer(collision.gameObject));
        }
    }

    private IEnumerator TeleportPlayer(GameObject player)
    {
        // Load the target scene asynchronously
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(targetSceneBuildIndex, LoadSceneMode.Additive);
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        // Set the target scene as the active scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(targetSceneBuildIndex));

        // Move the player to the SpawnPoint in the new scene
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
        if (spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
        }
        else
        {
            Debug.LogWarning("No SpawnPoint found in the target scene!");
        }

        // Unload the current scene
        if (SceneManager.GetSceneByBuildIndex(currentSceneBuildIndex).isLoaded)
        {
            SceneManager.UnloadSceneAsync(currentSceneBuildIndex);
        }
    }
}
