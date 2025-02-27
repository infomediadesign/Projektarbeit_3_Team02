using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnMenuPress()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OnResumePress()
    {
        //respawn logik
    }
}
