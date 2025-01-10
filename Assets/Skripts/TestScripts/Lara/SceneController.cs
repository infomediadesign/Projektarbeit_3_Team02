using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private CameraManager cameraManager;

    private void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainMenu":
            case "Level1":
            case "Level2_1":
                cameraManager.SwitchToCamera1();
                break;
            case "Level2_2":
            case "Level3":
                cameraManager.SwitchToCamera2();
                break;
        }
    }
}