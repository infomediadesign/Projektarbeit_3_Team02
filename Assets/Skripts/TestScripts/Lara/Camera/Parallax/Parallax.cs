using UnityEngine;
using Unity.Cinemachine; // Cinemachine Namespace hinzufügen

public class Parallax : MonoBehaviour
{
    [HideInInspector] public Camera mainCamera;
    private float length, startpos;
    public float parallaxEffect;
    void Start()
    {
        // Finde die Main Camera, die das CinemachineBrain hat
        // Diese Methode funktioniert auch, wenn die Main Camera in einem persistent data prefab ist
        CinemachineBrain brain = GameObject.FindObjectOfType<CinemachineBrain>();
        if (brain != null)
        {
            mainCamera = brain.GetComponent<Camera>();
            if (mainCamera != null)
            {
                Debug.Log("Found camera with CinemachineBrain: " + mainCamera.name);
            }
        }

        // Fallback zur MainCamera, falls keine Cinemachine Camera gefunden wurde
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera != null)
            {
                Debug.Log("Fallback to main camera: " + mainCamera.name);
            }
        }

        if (mainCamera == null)
        {
            Debug.LogWarning("Could not find camera for parallax effect on: " + gameObject.name);
            return;
        }

        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        if (mainCamera == null) return;  // Sicherheitscheck
        float temp = (mainCamera.transform.position.x * (1 - parallaxEffect));
        float dist = (mainCamera.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        /*
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
        */
    }
}