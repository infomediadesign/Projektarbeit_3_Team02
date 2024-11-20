using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    public int sceneBuildIndex; //Using build index instead of Level name -> we can still rename Levels

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger entered");   //Debug

        if(other.tag == "Player")   //Checks for Player tag
        {
            print("Switching Scene to " + sceneBuildIndex); //Debug

            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);  
        }
    }
}
