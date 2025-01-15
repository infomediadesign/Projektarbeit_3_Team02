using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class WeaponHitbox : EnemyBase
{
    protected PlayerHealth playerHealth;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Attack()
    {
    }
    public override void StopAttack()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("trigger");
            playerHealth = other.GetComponent<PlayerHealth>();

           
                playerHealth.TakeDamage(stats.damage);
                Debug.Log("taking damage: " + stats.damage);
            
            
        }
    }
}
