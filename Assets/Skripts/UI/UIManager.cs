using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
     public static UIManager Instance { get; private set; }
    private MemoryUIText memoryUIText;
    private LifeUIText lifeUIText;

    private void Start()
    {
        MusicManager.Instance.PlayMusic("MainMenu");

        memoryUIText = Object.FindFirstObjectByType<MemoryUIText>();
        lifeUIText = Object.FindFirstObjectByType<LifeUIText>();

        if (memoryUIText == null)
        {
            Debug.LogError("MemoryUIText not found in the scene.");
        }
        if (lifeUIText == null)
        {
            Debug.LogError("LifeUIText not found in the scene.");
        }

        if (memoryUIText != null)
        {
            EventManager.Instance.StartListening<int>("MemoryCollected", memoryUIText.IncrementMemoryCount);
        }
        if (lifeUIText != null)
        {
            EventManager.Instance.StartListening<int>("LifeCollected", lifeUIText.IncrementLifeCount);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
        MusicManager.Instance.PlayMusic("Level1");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    private void OnDestroy()
    {
        if (EventManager.Instance != null)
        {
            if (memoryUIText != null)
            {
                EventManager.Instance.StopListening<int>("MemoryCollected", memoryUIText.IncrementMemoryCount);
            }
            if (lifeUIText != null)
            {
                EventManager.Instance.StopListening<int>("LifeCollected", lifeUIText.IncrementLifeCount);
            }
        }
    }
}

