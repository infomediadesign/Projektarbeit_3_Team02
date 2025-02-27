using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    private float currentHealth;
    public float maxHealth;
    private StateManager state;
    private bool firstDeath;
  
    private void Start()
    { 
        state = GetComponent<StateManager>();
        currentHealth = maxHealth;
        // Subscribe with a parameter
        EventManager.Instance.StartListening<int>("LifeCollected", HealPlayer);

        // Initiales Event ausl�sen, damit die Gesundheitsanzeige aktualisiert wird
        EventManager.Instance.TriggerEvent<int>("HealthChanged", (int)currentHealth);
        firstDeath = false;

    }

    private void OnDestroy()
    {
        // Unsubscribe with a parameter
        EventManager.Instance.StopListening<int>("LifeCollected", HealPlayer);
    }
    public void TakeDamage(float damage)
    {
        if(currentHealth > 0)
        {
            state.damageAnim = true;
            if (!state.shielded && currentHealth > 0)
            {
                currentHealth -= damage;
                // Event ausl�sen, wenn sich die Gesundheit �ndert
                EventManager.Instance.TriggerEvent<int>("HealthChanged", (int)currentHealth);
            }
        }
             
        if (currentHealth <= 0)
        {
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
        
        // Event ausl�sen, wenn sich die Gesundheit �ndert
        EventManager.Instance.TriggerEvent<int>("HealthChanged", (int)currentHealth);
    }

    private void Die()
    {
        if (!firstDeath)
        {
            state.deathAnim = true;
            firstDeath = true;
        }


            //zoom
            StartCoroutine(DieSequence());
    }

    private IEnumerator DieSequence()
    {
        
        yield return new WaitForSeconds(3f); 
        SceneManager.LoadScene("GameOver");  
    }
}

