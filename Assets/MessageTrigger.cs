using UnityEngine;
using System.Collections;

public class MessageTrigger : MonoBehaviour
{
    public GameObject messageImageKeyboard; // Referenz zum UI-Image im Canvas
    public GameObject messageImageController; // Referenz zum UI-Image im Canvas
    public GameObject firstMessageImageKeyboard; // Referenz zum UI-Image im Canvas
    public GameObject firstMessageImageController;
    public float displayDuration = 3f; // Dauer in Sekunden, wie lange das Bild angezeigt wird

    private bool hasBeenTriggered = false; // Prüft, ob der Trigger schon aktiviert wurde
    private bool firstMessage = false;
    public static bool gameOver = false;

    private void Start()
    {
        // Bild am Anfang unsichtbar machen
        if (messageImageKeyboard != null)
        {
            messageImageKeyboard.SetActive(false);
        }
        else if(messageImageController != null)
        {
            messageImageController.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Überprüfen, ob der Spieler in den Trigger läuft und der Trigger noch nicht aktiviert wurde
        if (other.CompareTag("Player") && !hasBeenTriggered)
        {
            hasBeenTriggered = true; // Markieren, dass der Trigger bereits aktiviert wurde
            StartCoroutine(ShowMessage());
        }
    }
    private void Update()
    {
        if (!firstMessage)
        {
            if (IntroBildHandler.introFinished)
            {
                StartCoroutine(ShowFirstMessage());
                firstMessage = true;
            }
        }
        if (gameOver)
        {
            firstMessage = false;
            gameOver = false;
        }
    }

    private IEnumerator ShowMessage()
    {
        if (IntroBildHandler.introFinished)
        {
            if (!InputCheck.controller)
            {
                // Bild einblenden
                if (messageImageKeyboard != null)
                {
                    messageImageKeyboard.SetActive(true);
                }

                // Warten für die angegebene Dauer
                yield return new WaitForSeconds(displayDuration);

                // Bild ausblenden
                if (messageImageKeyboard != null)
                {
                    messageImageKeyboard.SetActive(false);
                }
            }
            else
            {
                // Bild einblenden
                if (messageImageController != null)
                {
                    messageImageController.SetActive(true);
                }

                // Warten für die angegebene Dauer
                yield return new WaitForSeconds(displayDuration);

                // Bild ausblenden
                if (messageImageController != null)
                {
                    messageImageController.SetActive(false);
                }
            }
        }
       
        
    }
    private IEnumerator ShowFirstMessage()
    {

            yield return new WaitForSeconds(1f);
            if (!InputCheck.controller)
            {
                // Bild einblenden
                if (firstMessageImageKeyboard != null)
                {
                    firstMessageImageKeyboard.SetActive(true);
                }

                // Warten für die angegebene Dauer
                yield return new WaitForSeconds(displayDuration);

                // Bild ausblenden
                if (firstMessageImageKeyboard != null)
                {
                    firstMessageImageKeyboard.SetActive(false);
                }
            }
            else
            {
                // Bild einblenden
                if (firstMessageImageController != null)
                {
                    firstMessageImageController.SetActive(true);
                }

                // Warten für die angegebene Dauer
                yield return new WaitForSeconds(displayDuration);

                // Bild ausblenden
                if (firstMessageImageController != null)
                {
                    firstMessageImageController.SetActive(false);
                }
            }

    }
}