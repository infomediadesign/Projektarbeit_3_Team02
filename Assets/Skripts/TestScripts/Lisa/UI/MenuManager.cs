using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        if (SceneManager.GetSceneByName("Tech Demo Level 1").isLoaded == false)
        {
            SceneManager.LoadScene("Tech Demo Level 1", LoadSceneMode.Additive);
        }
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
    }
}
