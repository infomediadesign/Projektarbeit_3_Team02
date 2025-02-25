using UnityEngine;
using System.Collections;

public class MessageTrigger : MonoBehaviour
{
    public GameObject messageImage; // Referenz zum UI-Image im Canvas
    public float displayDuration = 3f; // Dauer in Sekunden, wie lange das Bild angezeigt wird

    private bool hasBeenTriggered = false; // Prüft, ob der Trigger schon aktiviert wurde

    private void Start()
    {
        // Bild am Anfang unsichtbar machen
        if (messageImage != null)
        {
            messageImage.SetActive(false);
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

    private IEnumerator ShowMessage()
    {
        // Bild einblenden
        if (messageImage != null)
        {
            messageImage.SetActive(true);
        }

        // Warten für die angegebene Dauer
        yield return new WaitForSeconds(displayDuration);

        // Bild ausblenden
        if (messageImage != null)
        {
            messageImage.SetActive(false);
        }
    }
}