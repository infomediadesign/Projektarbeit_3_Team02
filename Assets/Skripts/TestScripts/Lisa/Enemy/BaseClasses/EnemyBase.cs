using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using System.Collections;

public abstract class EnemyBase : MonoBehaviour
{
    public EnemyStats stats;
    protected float currentHealth;
    protected bool counterPossible;
    protected bool isDying;
    protected bool deathAnimPlayed;
    protected SpriteRenderer sRenderer;
    private float flashDuration = 0.5f;
    protected virtual void Start()
    {
        currentHealth = stats.maxHealth;
        isDying = false;
        deathAnimPlayed = false;
        sRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(FreezeAnimation(0.3f));
        StartCoroutine(FlashRed());
        

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

    private IEnumerator FlashRed()
    {
        sRenderer.color = Color.yellow; 
        yield return new WaitForSeconds(flashDuration); 
        sRenderer.color = Color.white; 
    }
    private IEnumerator FreezeAnimation(float duration)
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.speed = 0; 
            yield return new WaitForSecondsRealtime(duration);
            animator.speed = 1; 
        }
    }

    public abstract void Attack();
    public abstract void StopAttack();
}
