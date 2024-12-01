using UnityEngine;

public class StationaryEnemy : EnemyBase
{
    private PlayerHealth playerHealth;

    void Update()
    {
        
    }

    public override void Attack()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("trigger");
            playerHealth = other.GetComponent<PlayerHealth>();
         
            playerHealth.TakeDamage(stats.damage);
            Debug.Log("taking damage");
          
        }
    }
}
