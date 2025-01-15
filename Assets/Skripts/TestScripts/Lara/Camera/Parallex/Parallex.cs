using UnityEngine;
public class Parallex : MonoBehaviour
{
    [HideInInspector] public Camera mainCamera;  // Umbenennung für Klarheit
    private float length, startpos;
    public float parallexEffect;

    void Start()
    {
        // Finde die Kamera
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera")?.GetComponent<Camera>();
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

        float temp = (mainCamera.transform.position.x * (1 - parallexEffect));
        float dist = (mainCamera.transform.position.x * parallexEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}