using UnityEngine;

public class CameraFollowing_horizontal : MonoBehaviour
{

    public float FollowSpeed = 2f;
    public Transform target;
    private float fixedY; 

    void Start()
    {
        fixedY = transform.position.y; // Stores the initial Y position of the camera when the game starts
    }

    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, fixedY, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
