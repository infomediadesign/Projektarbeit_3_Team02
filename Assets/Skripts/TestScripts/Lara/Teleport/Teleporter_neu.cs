using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.Cinemachine;

public class Teleporter_neu : MonoBehaviour
{
    public int targetSceneBuildIndex;
    public int currentSceneBuildIndex;

    // LoadingScreen Parameter
    public float loadingScreenDuration = 2.0f; // Dauer in Sekunden, einstellbar im Inspector
    public float fadeOutDuration = 1.0f;      // Dauer des Fade-Effekts

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

            // Direktes Update der Cinemachine Camera
            var vcam = GameObject.FindObjectOfType<CinemachineCamera>();
            if (vcam != null)
            {
                // Setze den Follow-Target direkt
                vcam.Follow = player.transform;
                // Optional: Hartes Reset der Kameraposition
                vcam.transform.position = new Vector3(
                    player.transform.position.x,
                    player.transform.position.y,
                    vcam.transform.position.z
                );
                // Force Cinemachine Update
                vcam.PreviousStateIsValid = false;
            }

            Debug.Log($"Player teleported to spawn point in scene {targetScene.name}");

            // Hier beginnt der neue Code für den LoadingScreen
            StartCoroutine(HandleLoadingScreen(targetScene));
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

    private IEnumerator HandleLoadingScreen(Scene targetScene)
    {
        // Suche nach dem LoadingScreen GameObject in der Ziel-Scene
        GameObject loadingScreen = null;
        GameObject[] rootObjects = targetScene.GetRootGameObjects();

        foreach (GameObject obj in rootObjects)
        {
            if (obj.CompareTag("LoadingScreen"))
            {
                loadingScreen = obj;
                break;
            }

            // Suche auch in Kindern
            Transform foundTransform = obj.transform.Find("LoadingScreen");
            if (foundTransform != null && foundTransform.CompareTag("LoadingScreen"))
            {
                loadingScreen = foundTransform.gameObject;
                break;
            }
        }

        if (loadingScreen != null)
        {
            // Aktiviere den LoadingScreen
            loadingScreen.SetActive(true);

            // Hole das SpriteRenderer Component
            SpriteRenderer spriteRenderer = loadingScreen.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                // Falls der LoadingScreen kein direktes SpriteRenderer hat, suche in Kindern
                spriteRenderer = loadingScreen.GetComponentInChildren<SpriteRenderer>();
            }

            // Warte für die eingestellte Dauer
            yield return new WaitForSeconds(loadingScreenDuration);

            // Fade-Out Effekt
            if (spriteRenderer != null)
            {
                Color originalColor = spriteRenderer.color;
                float elapsedTime = 0;

                while (elapsedTime < fadeOutDuration)
                {
                    elapsedTime += Time.deltaTime;
                    float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
                    spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                    yield return null;
                }
            }

            // Deaktiviere den LoadingScreen
            loadingScreen.SetActive(false);

            // Setze die Farbe zurück für die nächste Verwendung
            if (spriteRenderer != null)
            {
                Color originalColor = spriteRenderer.color;
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
            }
        }
        else
        {
            Debug.LogWarning("Kein GameObject mit dem Tag 'LoadingScreen' in der Zielszene gefunden.");
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