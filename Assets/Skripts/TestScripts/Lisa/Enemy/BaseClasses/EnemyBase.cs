using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public EnemyStats stats;
    protected float currentHealth;
    protected virtual void Start()
    {
        currentHealth = stats.maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public abstract void Attack();
    public abstract void StopAttack();
}
