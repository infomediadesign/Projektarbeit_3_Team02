using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public EnemyStats stats;
    protected float currentHealth;
    protected bool counterPossible;
    protected bool isDying;
    protected bool deathAnimPlayed;
    protected virtual void Start()
    {
        currentHealth = stats.maxHealth;
        isDying = false;
        deathAnimPlayed = false;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isDying = true;
   
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    public bool GetCounterPossible()
    {
        return counterPossible;
    }

    public abstract void Attack();
    public abstract void StopAttack();
}
