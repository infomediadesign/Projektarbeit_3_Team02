using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button[] defaultSoundButtons; //buttons will use the default sound
    [SerializeField] private string defaultButtonSoundEvent = "ButtonClicked"; //event for default sound (for example: start game button)

    public static UIManager Instance { get; private set; }
    public GameObject mainMenuScreen;
    public GameObject optionsScreen;
    public GameObject audioScreen; 
    public GameObject controlsScreen;
    public GameObject memoriesScreen;
    public GameObject creditsScreen;
    public GameObject exitgameScreen;
    public GameObject pauseScreen;


    private MemoryUIText memoryUIText;
    private LifeUIText lifeUIText;
    private bool isGamePaused = false;

    private void Start()
    {
        //ensure that main menu is active at start
       // ShowScreen(mainMenuScreen);
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
        EventManager.Instance.StartListening<string>("ShowScreen", HandleShowScreen);
        EventManager.Instance.StartListening("PauseGame", OnEnterPausePress);
        EventManager.Instance.StartListening("ResumeGame", OnGameResumePress);
        RegisterDefaultButtons();
    }

   
    private void HandleShowScreen(string screenName)
    {
        //activate screen based on screen name
        if (screenName == "MAIN MENU SCREEN")
        {
            ShowScreen(mainMenuScreen);
        }
        else if (screenName == "OPTIONS SCREEN")
        {
            ShowScreen(optionsScreen);
        }
        else if (screenName == "AUDIO SCREEN")
        {
            ShowScreen(audioScreen);
        }
         else if (screenName == "CONTROLS SCREEN")
        {
            ShowScreen(controlsScreen);
        }
        else if (screenName == "MEMORIES SCREEN")
        {
            ShowScreen(memoriesScreen);
        }
         else if (screenName == "CREDITS SCREEN")
        {
            ShowScreen(creditsScreen);
        }
         else if (screenName == "EXIT GAME SCREEN")
        {
            ShowScreen(exitgameScreen);
        }
    }

    private void RegisterDefaultButtons()
    {
        foreach (var button in defaultSoundButtons)
        {
            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    EventManager.Instance.TriggerEvent(defaultButtonSoundEvent);
                });
            }
        }
    }

    public void ShowScreen(GameObject screenToShow)
    {
        // Deactivate all screens
        mainMenuScreen.SetActive(false);
        optionsScreen.SetActive(false);
        audioScreen.SetActive(false);
        controlsScreen.SetActive(false);
        memoriesScreen.SetActive(false);
        creditsScreen.SetActive(false);
        exitgameScreen.SetActive(false);

        // Activate the requested screen
        screenToShow.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
        MusicManager.Instance.PlayMusic("Game");
        //MusicManager.Instance.PlayMusic("GameBackground");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void OnEnterPausePress()
    {
        Time.timeScale = 0; //pause game
        pauseScreen.SetActive(true);
        isGamePaused = true;
    }

    public void OnGameResumePress()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1; // Resume the game
            pauseScreen.SetActive(false);
            isGamePaused = false;
        }
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

            EventManager.Instance.StartListening<string>("ShowScreen", HandleShowScreen);
            EventManager.Instance.StopListening("PauseGame", OnEnterPausePress);
            EventManager.Instance.StopListening("ResumeGame", OnGameResumePress);
        }
    }
}

