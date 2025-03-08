using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(CinemachineBrain))]
public class CameraSelector : MonoBehaviour
{
    [Tooltip("Tag der Kamera, die am Anfang aktiviert werden soll")]
    [SerializeField] private string initialCameraTag = "Camera1";

    [Tooltip("Verz�gerung, bevor die initiale Kamera gesetzt wird (in Sekunden)")]
    [SerializeField] private float initDelay = 0.1f;

    private CinemachineBrain cinemachineBrain;

    private void Awake()
    {
        cinemachineBrain = GetComponent<CinemachineBrain>();

        if (cinemachineBrain == null)
        {
            Debug.LogError("Kein CinemachineBrain auf diesem GameObject gefunden!");
            return;
        }

        // Kurze Verz�gerung, um sicherzustellen, dass alle Kameras initialisiert sind
        StartCoroutine(SetInitialCameraWithDelay());
    }

    private IEnumerator SetInitialCameraWithDelay()
    {
        yield return new WaitForSeconds(initDelay);
        SetCameraWithTag(initialCameraTag);
    }

    public void SetCameraWithTag(string cameraTag)
    {
        // Alle Virtual Cameras in der Szene finden
        CinemachineCamera[] allCameras = FindObjectsOfType<CinemachineCamera>();

        foreach (CinemachineCamera cam in allCameras)
        {
            // Die Kamera mit dem gesuchten Tag finden
            if (cam.gameObject.CompareTag(cameraTag))
            {
                // Die Priorit�t dieser Kamera maximieren
                int highPriority = 999;
                cam.Priority = highPriority;

                Debug.Log("Cinemachine Kamera mit Tag '" + cameraTag + "' wurde aktiviert: " + cam.name);

                // Alle anderen Kameras auf eine niedrigere Priorit�t setzen
                foreach (CinemachineCamera otherCam in allCameras)
                {
                    if (otherCam != cam)
                    {
                        otherCam.Priority = 10; // Niedrigere Standardpriorit�t
                    }
                }

                return;
            }
        }

        Debug.LogWarning("Keine Cinemachine Kamera mit Tag '" + cameraTag + "' gefunden!");
    }

    // Diese Methode kann auch von anderen Skripten oder Events aufgerufen werden,
    // um die aktive Kamera w�hrend der Laufzeit zu �ndern
    public void SwitchToCamera(string cameraTag)
    {
        SetCameraWithTag(cameraTag);
    }
}