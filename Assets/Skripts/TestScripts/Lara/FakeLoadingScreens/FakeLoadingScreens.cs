using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FakeLoadingScreens : MonoBehaviour
{
    [Header("Loading Screen Settings")]
    [SerializeField] private Image loadingImage;
    [SerializeField] private float displayDuration = 3.0f;
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private int sceneIndexToLoad = -1;

    [Header("Optional Settings")]
    [SerializeField] private bool autoStart = true;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null && loadingImage != null)
            {
                canvasGroup = loadingImage.GetComponent<CanvasGroup>();
            }

            if (canvasGroup == null)
            {
                Debug.LogWarning("No CanvasGroup found. Adding one to the loading image.");
                canvasGroup = loadingImage.gameObject.AddComponent<CanvasGroup>();
            }
        }

        if (autoStart)
        {
            StartLoadingSequence();
        }
    }

    public void StartLoadingSequence()
    {
        StartCoroutine(LoadingSequence());
    }

    public void StartLoadingSequence(int sceneIndex)
    {
        sceneIndexToLoad = sceneIndex;
        StartLoadingSequence();
    }

    private IEnumerator LoadingSequence()
    {
        // Ensure everything is visible
        canvasGroup.alpha = 1;

        // Start loading the scene asynchronously in the background
        AsyncOperation asyncLoad = sceneIndexToLoad >= 0
            ? SceneManager.LoadSceneAsync(sceneIndexToLoad)
            : null;

        // If we're loading a scene, prevent it from activating immediately
        if (asyncLoad != null)
        {
            asyncLoad.allowSceneActivation = false;
        }

        // Wait for the display duration
        yield return new WaitForSeconds(displayDuration);

        // Fade out the loading screen
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure it's fully faded out
        canvasGroup.alpha = 0;
        // If we loaded a scene, activate it now
        if (asyncLoad != null)
        {
            asyncLoad.allowSceneActivation = true;
        }
        else
        {
            // If no scene to load, just deactivate the loading screen
            gameObject.SetActive(false);
        }
        
    }
}