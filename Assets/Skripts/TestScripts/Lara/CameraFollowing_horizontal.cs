using UnityEngine;

public class CameraFollowing_horizontal : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform target;

    [Header("Camera Bounds")]
    public Vector2 minBounds;    // Bottom-left corner of the boundary
    public Vector2 maxBounds;    // Top-right corner of the boundary

    private float halfWidth;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        AdjustCameraToFitBounds();
    }

    void AdjustCameraToFitBounds()
    {
        // Calculate the bounds height
        float boundsHeight = maxBounds.y - minBounds.y;

        // Set the camera's orthographic size to exactly half the bounds height
        // (orthographicSize is half the camera's height)
        cam.orthographicSize = boundsHeight * 0.5f;

        // Calculate half width based on the camera's aspect ratio
        halfWidth = cam.orthographicSize * cam.aspect;

        // Set the camera's Y position to the center of the bounds
        Vector3 pos = transform.position;
        pos.y = (minBounds.y + maxBounds.y) * 0.5f;
        transform.position = pos;
    }

    void Update()
    {
        // Get the target position, but maintain Y at center of bounds
        Vector3 desiredPosition = new Vector3(
            target.position.x,
            (minBounds.y + maxBounds.y) * 0.5f,
            -10f
        );

        // Clamp X position within bounds
        float clampedX = Mathf.Clamp(desiredPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        Vector3 clampedPosition = new Vector3(clampedX, desiredPosition.y, -10f);

        // Move the camera with smooth follow
        transform.position = Vector3.Slerp(transform.position, clampedPosition, FollowSpeed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            new Vector3((minBounds.x + maxBounds.x) * 0.5f, (minBounds.y + maxBounds.y) * 0.5f, 0f),
            new Vector3(maxBounds.x - minBounds.x, maxBounds.y - minBounds.y, 0f)
        );
    }
}