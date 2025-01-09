/*using UnityEngine;

[ExecuteInEditMode]
public class CameraBoundsHelper : MonoBehaviour
{
    public Color boundaryColor = Color.red;
    public bool showBoundary = true;

    [Header("Boundary Corners")]
    public Transform topRight;
    public Transform bottomLeft;

    private CameraFollowing_horizontal cameraScript;

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
            cameraScript = Camera.main.GetComponent<CameraFollowing_horizontal>();

        if (cameraScript != null)
        {
            cameraScript.minBounds = bottomLeft.position;
            cameraScript.maxBounds = topRight.position;

            // Preview the camera size in edit mode
            if (!Application.isPlaying && Camera.main != null)
            {
                float boundsHeight = topRight.position.y - bottomLeft.position.y;
                Camera.main.orthographicSize = boundsHeight * 0.5f;
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
            float height = topRight.position.y - bottomLeft.position.y;
            float width = height * Camera.main.aspect;
            Vector3 center = new Vector3(Camera.main.transform.position.x,
                                       (bottomLeft.position.y + topRight.position.y) * 0.5f,
                                       0f);
            Gizmos.DrawWireCube(center, new Vector3(width, height, 0.1f));
        }
    }
}*/