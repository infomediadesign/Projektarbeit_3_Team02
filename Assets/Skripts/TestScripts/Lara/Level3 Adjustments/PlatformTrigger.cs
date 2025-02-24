using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField] private GameObject platform; // Referenz zur Plattform

    private bool platformActivated = false; // Flag, um zu prüfen, ob die Plattform bereits aktiviert wurde

    void Start()
    {
        // Beim Start die Plattform deaktivieren
        if (platform != null)
        {
            // Deaktiviere den Renderer (unsichtbar)
            SpriteRenderer platformRenderer = platform.GetComponent<SpriteRenderer>();
            if (platformRenderer != null)
            {
                platformRenderer.enabled = false;
            }

            // Deaktiviere den Collider (keine Interaktion)
            Collider2D platformCollider = platform.GetComponent<Collider2D>();
            if (platformCollider != null)
            {
                platformCollider.enabled = false;
            }
        }
        else
        {
            Debug.LogError("Platform ist nicht zugewiesen! Bitte im Inspector zuweisen.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Überprüfen, ob es der Spieler ist (du kannst hier dein Tag anpassen)
        if (other.CompareTag("Player") && !platformActivated)
        {
            ActivatePlatform();
        }
    }

    void ActivatePlatform()
    {
        if (platform != null && !platformActivated)
        {
            // Aktiviere den Renderer (sichtbar)
            SpriteRenderer platformRenderer = platform.GetComponent<SpriteRenderer>();
            if (platformRenderer != null)
            {
                platformRenderer.enabled = true;
            }

            // Aktiviere den Collider (Interaktion)
            Collider2D platformCollider = platform.GetComponent<Collider2D>();
            if (platformCollider != null)
            {
                platformCollider.enabled = true;
            }

            // Setze Flag auf true, damit es nur einmal ausgelöst wird
            platformActivated = true;

            Debug.Log("Plattform wurde aktiviert!");
        }
    }
}