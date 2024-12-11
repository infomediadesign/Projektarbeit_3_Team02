using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    //Test
    private void Start()
    {
        MusicManager.Instance.PlayMusic("MainMenu");
    }

    public void Play()
    {
        //SceneManager.Instance.LoadSceneAsync("Game");
        MusicManager.Instance.PlayMusic("Game");
    }

}
