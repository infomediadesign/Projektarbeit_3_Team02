using UnityEngine;

public class UIManager : MonoBehaviour
{
      public static UIManager Instance { get; private set; }

    private MemoryUIText memoryUIText;
    private LifeUIText lifeUIText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //find the UI components
        MemoryUIText memoryUIText = Object.FindFirstObjectByType<MemoryUIText>();
        LifeUIText lifeUIText = Object.FindFirstObjectByType<LifeUIText>();

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

