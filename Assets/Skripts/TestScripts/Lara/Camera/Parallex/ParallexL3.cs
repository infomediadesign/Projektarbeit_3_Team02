using UnityEngine;
public class Parallex_L3 : MonoBehaviour
{
    private float length, startpos;
    public float parallexEffect;
    [HideInInspector] public Camera mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera")?.GetComponent<Camera>();
        if (mainCamera == null)
        {
            Debug.LogWarning("Could not find camera for parallax effect on: " + gameObject.name);
            return;
        }

        startpos = transform.position.y;  // Vertikale Position
        length = GetComponent<SpriteRenderer>().bounds.size.y;  // Vertikale Länge
    }

    void FixedUpdate()
    {
        if (mainCamera == null) return;  // Sicherheitscheck

        float temp = (mainCamera.transform.position.y * (1 - parallexEffect));
        float dist = (mainCamera.transform.position.y * parallexEffect);

        transform.position = new Vector3(transform.position.x, startpos + dist, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}