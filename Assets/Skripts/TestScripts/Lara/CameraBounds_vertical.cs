using UnityEngine;

[ExecuteInEditMode]
public class CameraBoundsHelper_vertical : MonoBehaviour
{
    public Color boundaryColor = Color.red;
    public bool showBoundary = true;

    [Header("Boundary Corners")]
    public Transform topRight;
    public Transform bottomLeft;

    private CameraFollowing_vertical cameraScript;

    void Update()
    {
        if (!Application.isPlaying && showBoundary)
        {
            UpdateCameraBounds();
        }
    }

    void UpdateCameraBounds()
    {
        if (topRight == null || bottomLeft == null) return;

        if (cameraScript == null)
            cameraScript = Camera.main.GetComponent<CameraFollowing_vertical>();

        if (cameraScript != null)
        {
            cameraScript.minBounds = bottomLeft.position;
            cameraScript.maxBounds = topRight.position;

            // Preview the camera size in edit mode
            if (!Application.isPlaying && Camera.main != null)
            {
                float boundsWidth = topRight.position.x - bottomLeft.position.x;
                Camera.main.orthographicSize = boundsWidth / (2 * Camera.main.aspect);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (!showBoundary || topRight == null || bottomLeft == null) return;

        // Draw boundary box (red)
        Gizmos.color = boundaryColor;
        Vector3 topLeft = new Vector3(bottomLeft.position.x, topRight.position.y, 0f);
        Vector3 bottomRight = new Vector3(topRight.position.x, bottomLeft.position.y, 0f);

        Gizmos.DrawLine(bottomLeft.position, topLeft);
        Gizmos.DrawLine(topLeft, topRight.position);
        Gizmos.DrawLine(topRight.position, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft.position);

        // Draw camera view bounds (green)
        if (Camera.main != null)
        {
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            float width = topRight.position.x - bottomLeft.position.x;
            float height = width / Camera.main.aspect;
            Vector3 center = new Vector3((bottomLeft.position.x + topRight.position.x) * 0.5f,
                                       Camera.main.transform.position.y,
                                       0f);
            Gizmos.DrawWireCube(center, new Vector3(width, height, 0.1f));
        }
    }
}