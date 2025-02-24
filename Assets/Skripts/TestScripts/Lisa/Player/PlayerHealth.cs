using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private float currentHealth;
    public float maxHealth;
    private StateManager state;
  
    private void Start()
    { 
        state = GetComponent<StateManager>();
        currentHealth = maxHealth;
        // Subscribe with a parameter
        EventManager.Instance.StartListening<int>("LifeCollected", HealPlayer);
    }

    private void OnDestroy()
    {
        // Unsubscribe with a parameter
        EventManager.Instance.StopListening<int>("LifeCollected", HealPlayer);
    }
    public void TakeDamage(float damage)
    {
        state.damageAnim = true;
        if (!state.shielded && currentHealth > 0)
        {
            currentHealth -= damage;
        }
       
        else if (currentHealth <= 0)
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
    }

    private void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}

