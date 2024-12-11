using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private float currentHealth;
    public float maxHealth;
    private StateManager state;
    //hier dann noch je nachdem healthslider oderso einf�gen
    private void Start()
    {
        state = GetComponent<StateManager>();
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        if (!state.shielded && currentHealth > 0)
        {
            currentHealth -= damage;
            
        }
       
        else if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth); // nicht �ber maxHelath
    }

    private void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}

