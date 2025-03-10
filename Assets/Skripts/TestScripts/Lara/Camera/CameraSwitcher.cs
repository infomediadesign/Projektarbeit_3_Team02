using System.Collections;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [Tooltip("Name der Cinemachine Kamera, zu der gewechselt werden soll")]
    [SerializeField] private string targetCameraName = "Cinemachine1";

    [Tooltip("Verzögerung in Sekunden vor dem Kamerawechsel nach dem Laden der Szene")]
    [SerializeField] private float switchDelay = 0.5f;

    private CameraSelector cameraSelector;

    private void Start()
    {
        // CameraSelector Referenz holen
        cameraSelector = FindObjectOfType<CameraSelector>();
    }

    /// <summary>
    /// Wechselt sofort zur eingestellten Zielkamera
    /// </summary>
    public void SwitchToTargetCamera()
    {
        // Zur konfigurierten Kamera wechseln, falls CameraSelector verfügbar ist
        if (cameraSelector != null)
        {
            cameraSelector.SwitchToCamera(targetCameraName);
        }
        else
        {
            // Falls kein CameraSelector gefunden wurde, versuche nach kurzer Verzögerung erneut
            StartCoroutine(SwitchCameraAfterDelay());
        }
    }

    /// <summary>
    /// Wechselt zur eingestellten Zielkamera nach dem Laden einer Szene
    /// </summary>
    public void SwitchToTargetCameraAfterSceneLoad()
    {
        StartCoroutine(SwitchCameraAfterDelay());
    }

    /// <summary>
    /// Wechselt zur angegebenen Kamera
    /// </summary>
    /// <param name="cameraName">Name der Zielkamera</param>
    public void SwitchToCamera(string cameraName)
    {
        // Temporäre Überschreibung des konfigurierten Kameranamens
        if (cameraSelector != null)
        {
            cameraSelector.SwitchToCamera(cameraName);
        }
        else
        {
            Debug.LogWarning("CameraSelector nicht gefunden. Kamerawechsel zu " + cameraName + " nicht möglich.");
        }
    }

    private IEnumerator SwitchCameraAfterDelay()
    {
        // Warten, bis die Szene geladen ist
        yield return new WaitForSeconds(switchDelay);

        // CameraSelector erneut suchen (falls er in einer neuen Szene existiert)
        cameraSelector = FindObjectOfType<CameraSelector>();

        if (cameraSelector != null)
        {
            cameraSelector.SwitchToCamera(targetCameraName);
        }
        else
        {
            Debug.LogWarning("CameraSelector nicht gefunden. Kamerawechsel zu " + targetCameraName + " nicht möglich.");
        }
    }
}