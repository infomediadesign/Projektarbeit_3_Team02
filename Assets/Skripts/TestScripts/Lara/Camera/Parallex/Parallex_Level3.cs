using UnityEngine;
public class Parallex_Level3 : MonoBehaviour
{
    private float length_3, startpos_3;
    public GameObject cam_3;
    public float parallexEffect_3;

    void Start()
    {
        startpos_3 = transform.position.y;  // Geändert von x zu y
        length_3 = GetComponent<SpriteRenderer>().bounds.size.y;  // Geändert von x zu y
    }

    void FixedUpdate()
    {
        float temp = (cam_3.transform.position.y * (1 - parallexEffect_3));  // Geändert von x zu y
        float dist = (cam_3.transform.position.y * parallexEffect_3);  // Geändert von x zu y
        transform.position = new Vector3(transform.position.x, startpos_3 + dist, transform.position.z);  // x und y Position getauscht

        if (temp > startpos_3 + length_3) startpos_3 += length_3;
        else if (temp < startpos_3 - length_3) startpos_3 -= length_3;
    }
}