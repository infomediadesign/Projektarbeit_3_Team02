using UnityEngine;
using Unity.Cinemachine;

public class CameraForceFollow : MonoBehaviour
{
    private CinemachineCamera virtualCamera;
    public Transform player;
    public float followSpeed = 10f;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineCamera>();
    }

    void LateUpdate()
    {
        if (player != null && virtualCamera != null)
        {
            Vector3 targetPosition = new Vector3(
                player.position.x,
                player.position.y,
                virtualCamera.transform.position.z
            );

            virtualCamera.transform.position = Vector3.Lerp(
                virtualCamera.transform.position,
                targetPosition,
                followSpeed * Time.deltaTime
            );
        }
    }

    public void UpdatePlayerReference(GameObject newPlayer)
    {
        player = newPlayer.transform;
    }
}