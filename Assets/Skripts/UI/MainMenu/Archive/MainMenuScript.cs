using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
        MusicManager.Instance.PlayMusic("Level1");
    }

    //Test
    private void Start()
    {
        MusicManager.Instance.PlayMusic("MainMenu");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

}
