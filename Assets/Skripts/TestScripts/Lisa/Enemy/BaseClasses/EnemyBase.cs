using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public EnemyStats stats;
    protected float currentHealth;
    protected bool counterPossible;
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
    public bool GetCounterPossible()
    {
        return counterPossible;
    }
    public abstract void Attack();
    public abstract void StopAttack();
}
