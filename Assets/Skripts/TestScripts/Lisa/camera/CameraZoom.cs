using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class CameraZoom : MonoBehaviour
{
    private CinemachineBrain cinemachineBrain;
    private Camera mainCamera;
    public float zoomAmount = 2f; // Wieviel kleiner der Orthographic Size wird
    public float zoomDuration = 0.5f;
    private float defaultSize;

    void Start()
    {
        //cinemachineBrain = FindFirstObjectByType<CinemachineBrain>();
        //mainCamera = Camera.main;
        //defaultSize = mainCamera.orthographicSize;
    }

    public void ZoomIn()
    {
        StartCoroutine(ZoomEffect());
    }

    private IEnumerator ZoomEffect()
    {
        float elapsedTime = 0f;
        float targetSize = defaultSize - zoomAmount;

        // Sanftes Zoomen
        while (elapsedTime < zoomDuration)
        {
            mainCamera.orthographicSize = Mathf.Lerp(defaultSize, targetSize, elapsedTime / zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.orthographicSize = targetSize;

        yield return new WaitForSeconds(0.5f); // Warte kurz

        // Zoom zurücksetzen
        elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            mainCamera.orthographicSize = Mathf.Lerp(targetSize, defaultSize, elapsedTime / zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.orthographicSize = defaultSize;
    }
}
