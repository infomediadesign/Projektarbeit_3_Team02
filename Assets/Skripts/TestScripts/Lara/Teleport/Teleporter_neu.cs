using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.Cinemachine;

public class Teleporter_neu : MonoBehaviour
{
    public int targetSceneBuildIndex;
    public int currentSceneBuildIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TeleportPlayer(collision.gameObject));
        }
    }

    private IEnumerator TeleportPlayer(GameObject player)
    {
        // Load target scene asynchronously
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(targetSceneBuildIndex, LoadSceneMode.Additive);
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        // Get the target scene
        Scene targetScene = SceneManager.GetSceneByBuildIndex(targetSceneBuildIndex);
        SceneManager.SetActiveScene(targetScene);

        // Find SpawnPoint and teleport player
        GameObject spawnPoint = null;
        GameObject[] rootObjects = targetScene.GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            spawnPoint = FindSpawnPointInHierarchy(obj);
            if (spawnPoint != null) break;
        }

        if (spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;

        }
        else
        {
            Debug.LogError($"No SpawnPoint found in target scene {targetScene.name}!");
        }

        // Unload the current scene
        if (SceneManager.GetSceneByBuildIndex(currentSceneBuildIndex).isLoaded)
        {
            yield return SceneManager.UnloadSceneAsync(currentSceneBuildIndex);
        }
    }

    private GameObject FindSpawnPointInHierarchy(GameObject obj)
    {
        if (obj.CompareTag("SpawnPoint"))
            return obj;

        // Search in children
        foreach (Transform child in obj.transform)
        {
            GameObject result = FindSpawnPointInHierarchy(child.gameObject);
            if (result != null)
                return result;
        }

        return null;
    }
}
