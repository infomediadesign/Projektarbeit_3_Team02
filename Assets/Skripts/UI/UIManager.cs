using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
      public static UIManager Instance { get; private set; }

    private MemoryUIText memoryUIText;
    private LifeUIText lifeUIText;

    private void Start()
    {
        //play main menu music
        MusicManager.Instance.PlayMusic("MainMenu");
        
        //find the UI components
        memoryUIText = Object.FindFirstObjectByType<MemoryUIText>();
        lifeUIText = Object.FindFirstObjectByType<LifeUIText>();

        //ensure they were found
        if (memoryUIText == null)
        {
            Debug.LogError("MemoryUIText not found in the scene.");
        }
        if (lifeUIText == null)
        {
            Debug.LogError("LifeUIText not found in the scene.");
        }

        //subscribe UI components to their events
        if (memoryUIText != null)
        {
            EventManager.Instance.StartListening("MemoryCollected", memoryUIText.IncrementMemoryCount);
        }
        if (lifeUIText != null)
        {
            EventManager.Instance.StartListening("LifeCollected", lifeUIText.IncrementLifeCount);
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
        //unsubscribe to avoid memory leaks
        if (EventManager.Instance != null)
        {
            if (memoryUIText != null)
            {
                EventManager.Instance.StopListening("MemoryCollected", memoryUIText.IncrementMemoryCount);
            }
            if (lifeUIText != null)
            {
                EventManager.Instance.StopListening("LifeCollected", lifeUIText.IncrementLifeCount);
            }
        }
    }
}

