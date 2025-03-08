using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public static bool startGamePressed;
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
        MusicManager.Instance.PlayMusic("Level1");
        startGamePressed = true;
    }

    //Test
    private void Start()
    {
        MusicManager.Instance.PlayMusic("MainMenu");
        startGamePressed = false;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

}
