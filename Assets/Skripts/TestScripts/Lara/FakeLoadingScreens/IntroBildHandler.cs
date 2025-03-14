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

    [Tooltip("Falls true, kann mit Mausklick zum nächsten Bild gesprungen werden")]
    public bool allowSkipWithClick = true;

    private Coroutine bilderSequenceCoroutine;
    private bool isShowingSequence = false;
    private int currentBildIndex = -1;
    private float currentBildTimer = 0f;
    private bool waitingForNextBild = false;

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
        // Auf Mausklick reagieren, wenn Sequenz läuft und Klick-Skip aktiviert ist
        if (isShowingSequence && allowSkipWithClick && Input.GetMouseButtonDown(0))
        {
            SkipToNextBild();
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

        // Starte die Sequenz
        bilderSequenceCoroutine = StartCoroutine(ShowBilderSequence());
    }

    private void SkipToNextBild()
    {
        // Wenn wir gerade auf das nächste Bild warten, unterbrechen wir dieses Warten
        if (waitingForNextBild)
        {
            waitingForNextBild = false;
            return;
        }

        // Aktuelles Bild ausblenden, wenn eines angezeigt wird
        if (currentBildIndex >= 0 && currentBildIndex < introBilder.Count)
        {
            var currentBild = introBilder[currentBildIndex];
            if (currentBild.spriteRenderer != null)
            {
                currentBild.spriteRenderer.enabled = false;
            }
        }

        // Zum nächsten Bild weitergehen
        currentBildIndex++;

        // Wenn wir am Ende der Sequenz angelangt sind, Sequenz beenden
        if (currentBildIndex >= introBilder.Count)
        {
            EndSequence();
            return;
        }

        // Nächstes Bild anzeigen
        var nextBild = introBilder[currentBildIndex];
        if (nextBild.spriteRenderer != null)
        {
            nextBild.spriteRenderer.enabled = true;
        }

        // Timer zurücksetzen
        currentBildTimer = 0f;
    }

    private void EndSequence()
    {
        // Alle Bilder ausblenden
        foreach (var bild in introBilder)
        {
            if (bild.spriteRenderer != null)
            {
                bild.spriteRenderer.enabled = false;
            }
        }

        // Sequenz als beendet markieren
        isShowingSequence = false;
        currentBildIndex = -1;
        bilderSequenceCoroutine = null;
    }

    private IEnumerator ShowBilderSequence()
    {
        isShowingSequence = true;
        currentBildIndex = -1;

        // Alle Bilder nacheinander anzeigen
        for (int i = 0; i < introBilder.Count; i++)
        {
            currentBildIndex = i;
            var bild = introBilder[i];

            // Aktuelles Bild anzeigen
            if (bild.spriteRenderer != null)
            {
                bild.spriteRenderer.enabled = true;
            }

            // Warte für die angegebene Zeit
            currentBildTimer = 0f;
            float targetDuration = bild.displayDuration;

            while (currentBildTimer < targetDuration)
            {
                waitingForNextBild = true;
                yield return null;

                // Wenn durch einen Klick unterbrochen, brechen wir die Warteschleife ab
                if (!waitingForNextBild)
                {
                    break;
                }

                currentBildTimer += Time.deltaTime;
            }
            waitingForNextBild = false;

            // Bild wieder ausblenden
            if (bild.spriteRenderer != null)
            {
                bild.spriteRenderer.enabled = false;
            }
        }

        EndSequence();
    }
}