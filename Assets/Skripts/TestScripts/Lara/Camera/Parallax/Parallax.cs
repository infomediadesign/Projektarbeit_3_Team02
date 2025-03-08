using UnityEngine;
using Unity.Cinemachine; // Cinemachine Namespace hinzufügen

public class Parallax : MonoBehaviour
{
    [HideInInspector] public Transform cameraTarget;  // Geändert zu Transform statt Camera
    private float length, startpos;
    public float parallaxEffect;

    void Start()
    {
        // Suche nach Cinemachine Virtual Camera
        CinemachineCamera virtualCamera = FindObjectOfType<CinemachineCamera>();
        if (virtualCamera != null)
        {
            cameraTarget = virtualCamera.transform;
            Debug.Log("Cinemachine Virtual Camera gefunden: " + virtualCamera.name);
        }
        else
        {
            // Alternativer Versuch: Brain finden
            CinemachineBrain cinemachineBrain = FindObjectOfType<CinemachineBrain>();
            if (cinemachineBrain != null)
            {
                cameraTarget = cinemachineBrain.transform;
                Debug.Log("Cinemachine Brain gefunden: " + cinemachineBrain.name);
            }
            else
            {
                // Fallback zur MainCamera
                Camera mainCamera = Camera.main;
                if (mainCamera != null)
                {
                    cameraTarget = mainCamera.transform;
                    Debug.Log("Fallback zur Main Camera: " + mainCamera.name);
                }
                else
                {
                    Debug.LogWarning("Keine Kamera für Parallax-Effekt gefunden auf: " + gameObject.name);
                    return;
                }
            }
        }

        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        if (cameraTarget == null) return;  // Sicherheitscheck

        float temp = (cameraTarget.position.x * (1 - parallaxEffect));
        float dist = (cameraTarget.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        /*
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
        */
    }
}