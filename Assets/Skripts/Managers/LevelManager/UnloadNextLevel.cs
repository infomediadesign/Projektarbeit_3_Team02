using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadNextLevel : MonoBehaviour
{
   public int lastBuildIndex;

   private void OnTriggerEnter2D(Collider2D collision)
   {
        if (collision.gameObject.name == "Player" && SceneManager.GetSceneByBuildIndex(lastBuildIndex).isLoaded) //Checks if Player is colliding with the checkpoint and if the scene is loaded
        {
            SceneManager.UnloadSceneAsync(lastBuildIndex); //Unloads last Scene
        }
   }
}
