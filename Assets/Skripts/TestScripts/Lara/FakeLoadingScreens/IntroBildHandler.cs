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
    private int currentBildIndex = 0;
    private bool skipRequested = false;

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

    private void Update()
    {
        // Überspringen mit Leertaste
        if (Input.GetKeyDown(KeyCode.Space) && bilderSequenceCoroutine != null)
        {
            skipRequested = true;
        }
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
        // Zurücksetzen der Variablen
        currentBildIndex = 0;
        skipRequested = false;
        // Starte die Sequenz
        bilderSequenceCoroutine = StartCoroutine(ShowBilderSequence());
    }
    private IEnumerator ShowBilderSequence()
    {
        // Alle Bilder nacheinander anzeigen
        for (currentBildIndex = 0; currentBildIndex < introBilder.Count; currentBildIndex++)
        {
            var bild = introBilder[currentBildIndex];

            // Aktuelles Bild anzeigen
            if (bild.spriteRenderer != null)
            {
                bild.spriteRenderer.enabled = true;
            }

            // Warte für die angegebene Zeit oder bis zum Überspringen
            float timer = 0f;
            skipRequested = false;

            while (timer < bild.displayDuration && !skipRequested)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            // Bild wieder ausblenden
            if (bild.spriteRenderer != null)
            {
                bild.spriteRenderer.enabled = false;
            }
        }

        introFinished = true;
        Debug.Log("setze finish" + introFinished);
        bilderSequenceCoroutine = null;
    }
}