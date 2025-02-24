using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayManager : MonoBehaviour
{
    [System.Serializable]
    public class HealthSprite
    {
        public GameObject spriteObject;
        public int maxHealth = 100;
        public int minHealth = 76;
    }

    // Array für die 5 Sprite-Zustände
    [SerializeField]
    private HealthSprite[] healthSprites = new HealthSprite[5];

    private void Start()
    {
        // Initialisiere alle Sprite-GameObjects als unsichtbar
        foreach (var healthSprite in healthSprites)
        {
            if (healthSprite.spriteObject != null)
            {
                healthSprite.spriteObject.SetActive(false);
            }
        }

        // Zeige das erste Sprite (volle Gesundheit) an bei Spielstart bis sich health ändert
        if (healthSprites.Length > 0 && healthSprites[0].spriteObject != null)
        {
            healthSprites[0].spriteObject.SetActive(true);
        }

        // Registriere die Methode für das HealthChanged-Event
        EventManager.Instance.StartListening<int>("HealthChanged", UpdateHealthDisplay);
    }

    private void OnDestroy()
    {
        // Beim Zerstören abmelden
        EventManager.Instance.StopListening<int>("HealthChanged", UpdateHealthDisplay);
    }

    // Diese Methode wird aufgerufen, wenn sich die Health ändert
    public void UpdateHealthDisplay(int currentHealth)
    {
        // Deaktiviere zuerst alle Sprites
        foreach (var healthSprite in healthSprites)
        {
            if (healthSprite.spriteObject != null)
            {
                healthSprite.spriteObject.SetActive(false);
            }
        }

        // Finde und aktiviere das passende Sprite für den aktuellen Health-Wert
        foreach (var healthSprite in healthSprites)
        {
            if (healthSprite.spriteObject != null)
            {
                if (currentHealth >= healthSprite.minHealth && currentHealth <= healthSprite.maxHealth)
                {
                    healthSprite.spriteObject.SetActive(true);
                    break;
                }
            }
        }
    }

    //Validierung der Werte im Inspector
    private void OnValidate()
    {
        if (healthSprites.Length != 5)
        {
            Debug.LogWarning("HealthDisplayManager benötigt genau 5 Sprite-Einträge!");
        }

        // Stelle sicher, dass die Werte in einem gültigen Bereich liegen
        foreach (var healthSprite in healthSprites)
        {
            healthSprite.maxHealth = Mathf.Clamp(healthSprite.maxHealth, 0, 100);
            healthSprite.minHealth = Mathf.Clamp(healthSprite.minHealth, 0, healthSprite.maxHealth);
        }
    }
}