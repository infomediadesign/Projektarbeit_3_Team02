using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine.Video;

public class TeleportLSCutscene : MonoBehaviour
{
    public int targetSceneBuildIndex;
    public int currentSceneBuildIndex;
    // Neues Feld für den Namen des zu aktivierenden Loading-Screens
    public string loadingScreenName;

    // Neue Felder für die Cutscene-Funktionalität
    [Header("Cutscene Einstellungen")]
    public bool playCutsceneBeforeLoading = false;
    public VideoClip cutsceneVideo;
    [Tooltip("Setzen Sie hier eine UI-Komponente, die das Video abspielen soll")]
    public VideoPlayer videoPlayer;
    [Tooltip("Canvas oder Panel, auf dem das Video angezeigt wird")]
    public GameObject videoCanvas;

    // Variable zum Verfolgen des Teleport-Status
    private bool isTeleporting = false;

    // Temporäres GameObject, das während des Video-Abspielens nicht zerstört wird
    private GameObject tempVideoHolder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTeleporting)
        {
            isTeleporting = true;

            if (playCutsceneBeforeLoading && cutsceneVideo != null && videoPlayer != null)
            {
                // Erstelle ein temporäres Objekt, das während des Scene-Wechsels bestehen bleibt
                tempVideoHolder = new GameObject("TempVideoHolder");
                DontDestroyOnLoad(tempVideoHolder);

                // Starte die komplette Sequenz mit Cutscene
                tempVideoHolder.AddComponent<VideoSequenceManager>().StartSequence(
                    this,
                    collision.gameObject,
                    cutsceneVideo,
                    videoPlayer,
                    videoCanvas,
                    loadingScreenName,
                    targetSceneBuildIndex,
                    currentSceneBuildIndex
                );
            }
            else
            {
                // Normaler Ablauf ohne Cutscene
                if (!string.IsNullOrEmpty(loadingScreenName))
                {
                    EventManager.Instance.TriggerEvent("ShowLoadingScreen", loadingScreenName);
                }
                StartCoroutine(TeleportPlayer(collision.gameObject));
            }
        }
    }

    // Diese Methode wird vom VideoSequenceManager aufgerufen, nachdem das Video abgespielt wurde
    public IEnumerator TeleportPlayer(GameObject player)
    {
        Debug.Log("Starte Teleportation");

        // Eine kurze Pause, um sicherzustellen, dass der Loading-Screen angezeigt wird
        yield return new WaitForSeconds(0.1f);

        // Load target scene asynchronously
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(targetSceneBuildIndex, LoadSceneMode.Additive);
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        Debug.Log("Neue Scene geladen");

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
            Debug.Log("Player zum SpawnPoint teleportiert");
        }
        else
        {
            Debug.LogError($"No SpawnPoint found in target scene {targetScene.name}!");
        }

        // Unload the current scene
        if (SceneManager.GetSceneByBuildIndex(currentSceneBuildIndex).isLoaded)
        {
            yield return SceneManager.UnloadSceneAsync(currentSceneBuildIndex);
            Debug.Log("Alte Scene entladen");
        }

        // Teleportation abgeschlossen
        isTeleporting = false;
        Debug.Log("Teleportation abgeschlossen");

        // Zerstöre das temporäre Video-Holder-Objekt, falls es existiert
        if (tempVideoHolder != null)
        {
            Destroy(tempVideoHolder);
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

// Diese Klasse übernimmt das Video-Abspielen und bleibt während des Scene-Wechsels bestehen
public class VideoSequenceManager : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private GameObject videoCanvas;
    private GameObject player;
    private TeleportLSCutscene teleporter;
    private string loadingScreenName;

    public void StartSequence(TeleportLSCutscene teleporterRef, GameObject playerRef, VideoClip video,
                             VideoPlayer vidPlayer, GameObject vidCanvas, string loadScreen,
                             int targetSceneIndex, int currentSceneIndex)
    {
        teleporter = teleporterRef;
        player = playerRef;
        loadingScreenName = loadScreen;

        // Klone den VideoPlayer und das Canvas, damit sie während des Scene-Wechsels bestehen bleiben
        videoPlayer = Instantiate(vidPlayer, transform);
        videoCanvas = Instantiate(vidCanvas, transform);

        // Stelle sicher, dass Video und Canvas im neuen Parent korrekt konfiguriert sind
        videoPlayer.clip = video;
        videoCanvas.SetActive(true);

        // Starte die Sequenz
        StartCoroutine(PlayVideoSequence());
    }

    private IEnumerator PlayVideoSequence()
    {
        Debug.Log("VideoSequenceManager: Starte Video-Sequenz");

        // Video vorbereiten und abspielen
        videoPlayer.Prepare();

        // Warten bis das Video vorbereitet ist
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        float videoLength = (float)videoPlayer.clip.length;
        Debug.Log($"VideoSequenceManager: Video ist vorbereitet, Länge: {videoLength} Sekunden");

        // Video abspielen
        videoPlayer.Play();

        // Warten, bis das Video komplett abgespielt wurde
        yield return new WaitForSeconds(videoLength + 0.5f);

        Debug.Log("VideoSequenceManager: Video-Wiedergabe abgeschlossen");

        // Video ausblenden
        videoCanvas.SetActive(false);

        // Loading Screen anzeigen
        if (!string.IsNullOrEmpty(loadingScreenName))
        {
            Debug.Log("VideoSequenceManager: Zeige Loading Screen an");
            EventManager.Instance.TriggerEvent("ShowLoadingScreen", loadingScreenName);

            // Kleine Pause für den Loading Screen
            yield return new WaitForSeconds(0.2f);
        }

        // Teleportation starten
        Debug.Log("VideoSequenceManager: Starte Teleportation");
        StartCoroutine(teleporter.TeleportPlayer(player));
    }
}