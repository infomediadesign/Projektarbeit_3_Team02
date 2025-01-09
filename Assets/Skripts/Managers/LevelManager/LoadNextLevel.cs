using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;  //Needed for Scene Management and switching scenes

public class LoadNextLevel : MonoBehaviour
{
    public int nextBuildIndex;
    public static bool loading;

    /*private void DeactivateOldCamera()
    {
        // Finde alle Virtual Cameras in der Scene
        CinemachineCamera[] cameras = Object.FindObjectsOfType<CinemachineCamera>();

        // Deaktiviere alle bis auf die neueste
        for (int i = 0; i < cameras.Length - 1; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !SceneManager.GetSceneByBuildIndex(nextBuildIndex).isLoaded) //checks if Player is colliding with checkpoint & if next scene is not loaded
        {
            if (loading)   //If the next scene is already loaded 
            {
                loading = false;
                return;
            }
            SceneManager.LoadScene(nextBuildIndex, LoadSceneMode.Additive); //Loads next scene without unloading the previous scene
            loading = true;
        }   
    }
}

