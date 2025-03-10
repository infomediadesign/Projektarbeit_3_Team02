using UnityEngine;

public class GrabManager : MonoBehaviour
{
    // Singleton-Pattern für einfachen Zugriff
    public static GrabManager Instance { get; private set; }

    // Referenz zur Holzfigur
    [SerializeField] private GameObject holzfigurObject;

    // SpriteRenderer der Holzfigur
    private SpriteRenderer holzfigurRenderer;

    // Event-Name für die Grab-Aktivierung (muss mit GrabDisplay übereinstimmen)
    private const string GRAVE_ACTIVATED_EVENT = "OnGraveActivated";

    private void Awake()
    {
        // Singleton-Setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Optional: Wenn dieses Objekt persistent sein soll
        DontDestroyOnLoad(gameObject);

        // Finde die Holzfigur, falls nicht im Inspector zugewiesen
        if (holzfigurObject == null)
        {
            holzfigurObject = GameObject.FindWithTag("Holzfigur");
        }

        // Zugriff auf den SpriteRenderer der Holzfigur
        if (holzfigurObject != null)
        {
            holzfigurRenderer = holzfigurObject.GetComponent<SpriteRenderer>();

            // Holzfigur zu Beginn unsichtbar machen
            if (holzfigurRenderer != null)
            {
                holzfigurRenderer.enabled = false;
            }
            else
            {
                Debug.LogError("Holzfigur hat keinen SpriteRenderer!");
            }
        }
        else
        {
            Debug.LogError("Holzfigur nicht gefunden! Stelle sicher, dass sie den Tag 'Holzfigur' hat.");
        }
    }

    private void OnEnable()
    {
        // EventManager abonnieren
        if (EventManager.Instance != null)
        {
            EventManager.Instance.StartListening(GRAVE_ACTIVATED_EVENT, OnGraveActivated);
            Debug.Log($"[GrabManager] Event '{GRAVE_ACTIVATED_EVENT}' abonniert");
        }
        else
        {
            Debug.LogWarning("[GrabManager] EventManager nicht gefunden. Direkter Aufruf wird verwendet.");
        }
    }

    private void OnDisable()
    {
        // EventManager-Abonnement entfernen
        if (EventManager.Instance != null)
        {
            EventManager.Instance.StopListening(GRAVE_ACTIVATED_EVENT, OnGraveActivated);
        }
    }

    // Methode, die vom GrabDisplay aufgerufen werden kann
    public void OnGraveActivated()
    {
        // Holzfigur sichtbar machen
        if (holzfigurRenderer != null)
        {
            holzfigurRenderer.enabled = true;
            Debug.Log("Holzfigur wurde sichtbar gemacht!");

            // Sound abspielen, wenn die Holzfigur gefunden wird            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySound2D("WoodenFigureFound");
                Debug.Log("Sound 'WoodenFigureFound' abgespielt");
            }
        }
    }
}