using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class PlayerFollow : MonoBehaviour
{
    private CinemachineCamera virtualCamera;

    [Tooltip("Tag des Players, der verfolgt werden soll")]
    [SerializeField] private string playerTag = "Player";

    [Tooltip("Verzögerung, bevor die Suche nach dem Player beginnt (in Sekunden)")]
    [SerializeField] private float searchDelay = 0.1f;

    [Tooltip("Maximale Anzahl von Versuchen, den Player zu finden")]
    [SerializeField] private int maxSearchAttempts = 10;

    private void Awake()
    {
        // Referenz zur Cinemachine Virtual Camera holen
        virtualCamera = GetComponent<CinemachineCamera>();

        if (virtualCamera == null)
        {
            Debug.LogError("Es wurde keine CinemachineVirtualCamera auf diesem GameObject gefunden!");
            return;
        }

        // Sofort versuchen, den Player zu finden
        if (!TryFindAndSetPlayer())
        {
            // Wenn nicht sofort gefunden, starte Coroutine für wiederholte Versuche
            StartCoroutine(FindPlayerWithRetry());
        }
    }

    private bool TryFindAndSetPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        if (player != null)
        {
            // Player gefunden - als Follow und LookAt Target setzen
            virtualCamera.Follow = player.transform;
            virtualCamera.LookAt = player.transform;

            Debug.Log("Player als Kamera-Target gesetzt: " + player.name);
            return true;
        }

        return false;
    }

    private IEnumerator FindPlayerWithRetry()
    {
        int attempts = 0;

        while (attempts < maxSearchAttempts)
        {
            yield return new WaitForSeconds(searchDelay);

            if (TryFindAndSetPlayer())
            {
                // Player gefunden, Suche beenden
                yield break;
            }

            attempts++;
        }

        Debug.LogWarning("Player konnte nach " + maxSearchAttempts + " Versuchen nicht gefunden werden!");
    }
}