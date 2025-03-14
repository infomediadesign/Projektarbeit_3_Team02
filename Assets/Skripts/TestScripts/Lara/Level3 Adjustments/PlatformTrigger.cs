using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField] private GameObject platform; // Referenz zur Plattform
    [SerializeField] private GameObject platformDelete; // Referenz zur Delete Plattform
    private CameraSelector cameraSelector;

    public static bool platformActivated = false; // Flag, um zu prüfen, ob die Plattform bereits aktiviert wurde

    void Start()
    {
        // Beim Start die Plattform deaktivieren
        if (platform != null)
        {
            // Deaktiviere den Renderer (unsichtbar)
            TilemapRenderer platformRenderer = platform.GetComponent<TilemapRenderer>();
            if (platformRenderer != null)
            {
                platformRenderer.enabled = false;
            }

            // Deaktiviere den Collider (keine Interaktion)
            TilemapCollider2D platformCollider = platform.GetComponent<TilemapCollider2D>();
            if (platformCollider != null)
            {
                platformCollider.enabled = false;
            }
        }
        else
        {
            Debug.LogError("Platform ist nicht zugewiesen! Bitte im Inspector zuweisen.");
        }

        // CameraSelector finden und zuweisen
        cameraSelector = FindObjectOfType<CameraSelector>();
        if (cameraSelector == null)
        {
            Debug.LogWarning("CameraSelector konnte nicht gefunden werden!");
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Überprüfen, ob es der Spieler ist (du kannst hier dein Tag anpassen)
        if (other.CompareTag("Player") && !platformActivated)
        {

            ActivatePlatform();
            DestroyPlatform();
            Destroy(gameObject);


            // Zur Cinemachine1 wechseln, falls CameraSelector verfügbar ist
            if (cameraSelector != null)
            {
                cameraSelector.SwitchToCamera("CineCamBoss");
                Debug.Log("CineCamBoss wurde aktiviert!");

            }
            else
            {
                Debug.Log("CineCamBoss wurde nicht gefunden! Suche CameraSelector...");
                // Falls der CameraSelector nach dem Szenenwechsel nicht mehr vorhanden ist
                // kannst du den CameraSelector in der neuen Szene suchen und aufrufen
                StartCoroutine(SwitchCameraAfterSceneLoad());
            }
        }
    }

    void ActivatePlatform()
    {
        if (platform != null && !platformActivated)
        {
            // Aktiviere den Renderer (sichtbar)
            TilemapRenderer platformRenderer = platform.GetComponent<TilemapRenderer>();
            if (platformRenderer != null)
            {
                platformRenderer.enabled = true;
            }

            // Aktiviere den Collider (Interaktion)
            TilemapCollider2D platformCollider = platform.GetComponent<TilemapCollider2D>();
            if (platformCollider != null)
            {
                platformCollider.enabled = true;
            }

            // Setze Flag auf true, damit es nur einmal ausgelöst wird
            platformActivated = true;

            Debug.Log("Plattform wurde aktiviert!");
            Boss.bossActive = true;

            //Destroy(gameObject);
        }
    }

    private IEnumerator SwitchCameraAfterSceneLoad()
    {
        // Warten, bis die Szene geladen ist
        yield return new WaitForSeconds(0.5f);

        // CameraSelector in der neuen Szene suchen
        CameraSelector newSceneCameraSelector = FindObjectOfType<CameraSelector>();
        if (newSceneCameraSelector != null)
        {
            newSceneCameraSelector.SwitchToCamera("CineCamBoss");
            Debug.Log("CameraSelector und CineCamBoss doch noch gefunden!");

        }
        else
        {
            Debug.LogWarning("CameraSelector in der neuen Szene nicht gefunden.");
        }
    }

    void DestroyPlatform()
    {
        if (platformDelete != null)
        {
            Destroy(platformDelete);
        }
    }
}