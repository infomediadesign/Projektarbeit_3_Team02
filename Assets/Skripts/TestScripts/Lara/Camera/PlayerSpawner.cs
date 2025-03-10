using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{
    // Bewegungskomponente (optional, falls vorhanden)
    private MonoBehaviour movementScript;

    // Anzahl der Frames, die wir warten wollen
    [SerializeField] private int framesToWait = 5;

    private void Awake()
    {
        // Versuche, die Bewegungskomponente zu finden (passe den Namen an, falls notwendig)
        // z.B. PlayerMovement, CharacterController, etc.
        movementScript = GetComponent<MonoBehaviour>();

        // Bewegung temporär deaktivieren, falls eine Komponente gefunden wurde
        if (movementScript != null)
        {
            movementScript.enabled = false;
            Debug.Log("Bewegungsscript temporär deaktiviert");
        }

        // Starte die Spieler-Positionierung
        StartCoroutine(PositionPlayerAtSpawnPoint());
    }

    private IEnumerator PositionPlayerAtSpawnPoint()
    {
        // Warte einige Frames, um sicherzustellen, dass alle Szenen und Physik initialisiert sind
        for (int i = 0; i < framesToWait; i++)
        {
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Positioniere Player nach " + framesToWait + " Frames...");

        // Suche nach dem SpawnPoint in Level 1 (Build Index 1)
        GameObject spawnPoint = null;

        // Prüfe, ob Level 1 bereits geladen ist
        bool level1Loaded = false;
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.buildIndex == 1)
            {
                level1Loaded = true;
                break;
            }
        }

        // Wenn Level 1 nicht geladen ist, geben wir eine Warnung aus
        if (!level1Loaded)
        {
            Debug.LogWarning("Level 1 (Build Index 1) ist nicht geladen!");
            EnableMovement();
            yield break;
        }

        // Suche nach allen GameObjects mit dem Tag SpawnPoint
        GameObject[] allSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        // Finde den SpawnPoint in der richtigen Szene
        foreach (GameObject potentialSpawnPoint in allSpawnPoints)
        {
            // Prüfe, ob dieses GameObject zu Level 1 gehört
            if (potentialSpawnPoint.scene.buildIndex == 1)
            {
                spawnPoint = potentialSpawnPoint;
                break;
            }
        }

        // Überprüfe, ob ein SpawnPoint gefunden wurde
        if (spawnPoint != null)
        {
            // Deaktiviere Rigidbody temporär, falls vorhanden
            Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            bool wasKinematic = false;

            if (rb2d != null)
            {
                wasKinematic = rb2d.isKinematic;
                rb2d.isKinematic = true;
                rb2d.linearVelocity = Vector2.zero;
            }

            // Setze die Position des Players auf die Position des SpawnPoints
            transform.position = spawnPoint.transform.position;

            // Warte mehrere Frames, um sicherzustellen, dass die Positionierung und Physik verarbeitet wird
            yield return new WaitForSeconds(0.1f);

            // Wir fixieren den Spieler am SpawnPoint für einen Moment
            transform.position = spawnPoint.transform.position;

            // Rigidbody Einstellungen zurücksetzen
            if (rb2d != null)
            {
                rb2d.isKinematic = wasKinematic;
            }

            // Debug-Information
            Debug.Log("Player wurde zum SpawnPoint in Level 1 positioniert: " + transform.position);
        }
        else
        {
            // Warnmeldung, falls kein SpawnPoint gefunden wurde
            Debug.LogWarning("Kein GameObject mit dem Tag 'SpawnPoint' in Level 1 gefunden!");
        }

        // Aktiviere die Bewegung wieder
        EnableMovement();
    }

    private void EnableMovement()
    {
        // Verzögere die Aktivierung um einen kurzen Moment
        StartCoroutine(DelayedEnableMovement());
    }

    private IEnumerator DelayedEnableMovement()
    {
        // Warte einen kurzen Moment, bevor wir die Bewegung aktivieren
        yield return new WaitForSeconds(0.2f);

        // Stelle sicher, dass der Spieler keine unerwünschte Geschwindigkeit hat
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            rb2d.linearVelocity = Vector2.zero;
        }

        // Aktiviere die Bewegungskomponente wieder, falls sie existiert
        if (movementScript != null)
        {
            movementScript.enabled = true;
            Debug.Log("Bewegungsscript wieder aktiviert");
        }
    }
}