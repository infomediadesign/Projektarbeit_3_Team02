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
    private float flashDuration = 0.2f;
    public BoxCollider2D attackCollider;
    protected bool attackCollision;
    protected virtual void Start()
    {
        attackCollision = false;
        currentHealth = stats.maxHealth;
        isDying = false;
        deathAnimPlayed = false;
        sRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(FreezeAnimation(0.2f));
        StartCoroutine(FlashRed());
        
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
            if (currentHealth <= 0)
            {
                 isDying = true;
            }
        }
    }
    public void EnableAttackCollider()
    {
    
            attackCollision = true;
        
    }
    public void DisableAttackCollider()
    {

            attackCollision = false;
        
    }

    public abstract void Attack();
    public abstract void StopAttack();
}
