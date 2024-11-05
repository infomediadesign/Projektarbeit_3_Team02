using UnityEngine;

public class Parallex : MonoBehaviour
{

    private float length, startpos;
    public GameObject cam;
    public float parallexEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpos = transform.position.x;
        length= GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = (cam.transform.position.x * parallexEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
    }
}
