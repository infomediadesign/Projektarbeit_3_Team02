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

    // Array f�r die 5 Sprite-Zust�nde
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

        // Zeige das erste Sprite (volle Gesundheit) an bei Spielstart bis sich health �ndert
        if (healthSprites.Length > 0 && healthSprites[0].spriteObject != null)
        {
            healthSprites[0].spriteObject.SetActive(true);
        }

        // Registriere die Methode f�r das HealthChanged-Event
        EventManager.Instance.StartListening<int>("HealthChanged", UpdateHealthDisplay);
    }

    private void OnDestroy()
    {
        // Beim Zerst�ren abmelden
        EventManager.Instance.StopListening<int>("HealthChanged", UpdateHealthDisplay);
    }

    // Diese Methode wird aufgerufen, wenn sich die Health �ndert
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

        // Finde und aktiviere das passende Sprite f�r den aktuellen Health-Wert
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
            Debug.LogWarning("HealthDisplayManager ben�tigt genau 5 Sprite-Eintr�ge!");
        }

        // Stelle sicher, dass die Werte in einem g�ltigen Bereich liegen
        foreach (var healthSprite in healthSprites)
        {
            healthSprite.maxHealth = Mathf.Clamp(healthSprite.maxHealth, 0, 100);
            healthSprite.minHealth = Mathf.Clamp(healthSprite.minHealth, 0, healthSprite.maxHealth);
        }
    }
}