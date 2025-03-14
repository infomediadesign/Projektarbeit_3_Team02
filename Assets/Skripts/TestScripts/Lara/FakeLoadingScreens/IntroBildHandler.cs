using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntroBildHandler : MonoBehaviour
{
    [System.Serializable]
    public class IntroBild
    {
        public SpriteRenderer spriteRenderer;
        [Tooltip("Zeit in Sekunden, die das Bild angezeigt wird")]
        public float displayDuration = 3.0f;
    }

    [Tooltip("Liste der Intro-Bilder in der Reihenfolge der Anzeige")]
    public List<IntroBild> introBilder = new List<IntroBild>();

    private Coroutine bilderSequenceCoroutine;
    public static bool introFinished = false;

    private void Awake()
    {
        // Alle SpriteRenderer zu Beginn deaktivieren
        foreach (var bild in introBilder)
        {
            if (bild.spriteRenderer != null)
            {
                bild.spriteRenderer.enabled = false;
            }
            else
            {
                Debug.LogError("Ein SpriteRenderer in der IntroBilder-Liste ist nicht zugewiesen!");
            }
        }
    }

    private void Start()
    {
        // Event-Listener registrieren
        EventManager.Instance.StartListening("ShowIntroBild", ShowIntroBilder);
    }

    private void OnDestroy()
    {
        // Event-Listener entfernen
        if (EventManager.Instance != null)
        {
            EventManager.Instance.StopListening("ShowIntroBild", ShowIntroBilder);
        }

        // Laufende Sequenz stoppen
        if (bilderSequenceCoroutine != null)
        {
            StopCoroutine(bilderSequenceCoroutine);
        }
    }

    private void ShowIntroBilder()
    {
        // Stoppe eine möglicherweise laufende Sequenz
        if (bilderSequenceCoroutine != null)
        {
            StopCoroutine(bilderSequenceCoroutine);
        }

        // Starte die Sequenz
        bilderSequenceCoroutine = StartCoroutine(ShowBilderSequence());
    }

    private IEnumerator ShowBilderSequence()
    {
        // Alle Bilder nacheinander anzeigen
        foreach (var bild in introBilder)
        {
            // Aktuelles Bild anzeigen
            if (bild.spriteRenderer != null)
            {
                bild.spriteRenderer.enabled = true;
            }

            // Warte für die angegebene Zeit
            yield return new WaitForSeconds(bild.displayDuration);

            // Bild wieder ausblenden
            if (bild.spriteRenderer != null)
            {
                bild.spriteRenderer.enabled = false;
            }
        }
        introFinished = true;
        Debug.Log("setze  finish" + introFinished);
        bilderSequenceCoroutine = null;
    }
}