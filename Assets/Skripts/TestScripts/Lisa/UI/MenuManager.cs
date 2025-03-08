using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool gameStarted;
    void Start()
    {

    
        if (SceneManager.GetSceneByName("Tech Demo Level 1").isLoaded == false)
        {
            gameStarted = false;
            SceneManager.LoadScene("Tech Demo Level 1", LoadSceneMode.Additive);
        }
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
        gameStarted = true;
    }
}
