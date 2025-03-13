using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    private float currentHealth;
    public float maxHealth;
    private StateManager state;
    private bool firstDeath;
    static public bool death;

    private float lastDamageTime = -999f; 
    private float damageCooldown = 1.5f;
    private void Start()
    { 
        state = GetComponent<StateManager>();
        currentHealth = maxHealth;
        // Subscribe with a parameter
        EventManager.Instance.StartListening<int>("LifeCollected", HealPlayer);

        // Initiales Event auslösen, damit die Gesundheitsanzeige aktualisiert wird
        EventManager.Instance.TriggerEvent<int>("HealthChanged", (int)currentHealth);
        firstDeath = false;
        death = false;

    }

    private void OnDestroy()
    {
        // Unsubscribe with a parameter
        EventManager.Instance.StopListening<int>("LifeCollected", HealPlayer);
    }
    public void TakeDamage(float damage)
    {
        if (Time.time - lastDamageTime < damageCooldown) return;

        lastDamageTime = Time.time; // Zeit aktualisieren
        if (currentHealth > 0)
        {
            if (!state.shielded)
            {
                state.damageAnim = true;
                if (currentHealth > 0 && !state.rolling)
                {
                    currentHealth -= damage;
                    // Event auslösen, wenn sich die Gesundheit ändert
                    EventManager.Instance.TriggerEvent<int>("HealthChanged", (int)currentHealth);
                }
            }
         
        }
             
        if (currentHealth <= 0)
        {
            death = true;
            Die();
        }
    }

    private void HealPlayer(int healAmount)
    {
        Heal(healAmount);
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth); // heal AMOUNT - nicht over maxHelath
        
        // Event auslösen, wenn sich die Gesundheit ändert
        EventManager.Instance.TriggerEvent<int>("HealthChanged", (int)currentHealth);
    }

    private void Die()
    {
        if (!firstDeath)
        {
            state.deathAnim = true;
            firstDeath = true;
        }


          
            StartCoroutine(DieSequence());
    }

    private IEnumerator DieSequence()
    {
        //FindFirstObjectByType<CameraZoom>().ZoomIn();
        yield return new WaitForSeconds(3f);
       // SceneManager.LoadScene("GameOver");
        FindObjectOfType<SceneFader>().FadeToScene("GameOver");
    }
}

