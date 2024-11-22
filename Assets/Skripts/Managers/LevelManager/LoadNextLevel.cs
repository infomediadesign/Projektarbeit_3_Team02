using UnityEngine;
using UnityEngine.SceneManagement;  //Needed for Scene Management ans switching scenes

public class LoadNextLevel : MonoBehaviour
{
    public int nextBuildIndex;
    public static bool loading;
    
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

