using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.Cinemachine;

public class Teleporter_neu : MonoBehaviour
{
    public int targetSceneBuildIndex;
    public int currentSceneBuildIndex;

    // Neues Feld für den Namen des zu aktivierenden Loading-Screens
    public string loadingScreenName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Zeige Loading-Screen an, bevor das Teleportieren beginnt
            if (!string.IsNullOrEmpty(loadingScreenName))
            {
                EventManager.Instance.TriggerEvent("ShowLoadingScreen", loadingScreenName);
            }

            StartCoroutine(TeleportPlayer(collision.gameObject));
        }
    }

    private IEnumerator TeleportPlayer(GameObject player)
    {
        // Eine kurze Pause, um sicherzustellen, dass der Loading-Screen angezeigt wird
        yield return new WaitForSeconds(0.1f);

        // Load target scene asynchronously
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(targetSceneBuildIndex, LoadSceneMode.Additive);
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        // Get the target scene
        Scene targetScene = SceneManager.GetSceneByBuildIndex(targetSceneBuildIndex);
        SceneManager.SetActiveScene(targetScene);
        if (targetSceneBuildIndex == 2)
        {
            MusicManager.Instance.PlayMusic("Level2.1", "GameBackground");
        }
        else if (targetSceneBuildIndex == 3)
        {
            MusicManager.Instance.PlayMusic("Level2.2", "GameBackground");
        }
        else if (targetSceneBuildIndex == 4)
        {
            MusicManager.Instance.PlayMusic("Level3", "GameBackground");
        }

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

        // Der Loading-Screen wird nun durch seinen eigenen Timer ausgeblendet
       
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