using UnityEngine;

public class CameraFollowing_vertical : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform target;

    [Header("Camera Bounds")]
    public Vector2 minBounds;    // Bottom-left corner of the boundary
    public Vector2 maxBounds;    // Top-right corner of the boundary

    private float halfHeight;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        AdjustCameraToFitBounds();
    }

    void AdjustCameraToFitBounds()
    {
        // Calculate the bounds width
        float boundsWidth = maxBounds.x - minBounds.x;

        // Set the camera's width to match the bounds width
        float targetOrthoSize = boundsWidth / (2 * cam.aspect);
        cam.orthographicSize = targetOrthoSize;

        // Calculate halfHeight for clamping
        halfHeight = cam.orthographicSize;

        // Set the camera's X position to the center of the bounds
        Vector3 pos = transform.position;
        pos.x = (minBounds.x + maxBounds.x) * 0.5f;
        transform.position = pos;
    }

    void Update()
    {
        // Get the target position, but maintain X at center of bounds
        Vector3 desiredPosition = new Vector3(
            (minBounds.x + maxBounds.x) * 0.5f,
            target.position.y,
            -10f
        );

        // Clamp Y position within bounds
        float clampedY = Mathf.Clamp(desiredPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        Vector3 clampedPosition = new Vector3(desiredPosition.x, clampedY, -10f);

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