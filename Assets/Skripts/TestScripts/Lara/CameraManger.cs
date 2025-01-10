using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineCamera CinemachineCamera1; // Für Main Menu, Level 1, Level 2.1
    public CinemachineCamera CinemachineCamera2; // Für Level 2.2, Level 3

    public void SwitchToCamera1()
    {
        CinemachineCamera1.Priority = 20;
        CinemachineCamera2.Priority = 10;
    }

    public void SwitchToCamera2()
    {
        CinemachineCamera1.Priority = 10;
        CinemachineCamera2.Priority = 20;
    }
}