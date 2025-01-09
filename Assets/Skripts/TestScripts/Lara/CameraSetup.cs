using UnityEngine;
using Unity.Cinemachine;

public class CameraSetup : MonoBehaviour
{
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CinemachineCamera vCam = GetComponent<CinemachineCamera>();

        if (player != null && vCam != null)
        {
            vCam.Follow = player.transform;
            Debug.Log("Camera successfully connected to player");
        }
        else
        {
            Debug.LogWarning("Could not find player or virtual camera!");
        }
    }
}