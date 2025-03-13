using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using System.Collections;

public abstract class EnemyBase : MonoBehaviour
{
    public GameObject respawnEnemyPrefab;
    public EnemyStats stats;
    protected float currentHealth;
    protected bool counterPossible;
    protected bool isDying;
    protected SpriteRenderer sRenderer;
    private float flashDuration = 0.2f;
    public BoxCollider2D attackCollider;
    protected bool attackCollision;
    public bool respawn;
    public float respawnTime;
    public bool isObstacle;

    protected virtual void Start()
    {
        attackCollision = false;
        currentHealth = stats.maxHealth;
        isDying = false;
        sRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void TakeDamage(int damage)
    {
        if (isDying) return;
        if (isObstacle)
        {
            currentHealth -= damage;
            if(currentHealth <= 0)
            {
                Die();
            }
        }
        else
        {
            currentHealth -= damage;
            StartCoroutine(FreezeAnimation(0.2f));
            StartCoroutine(FlashRed());
        }
        
    }

    protected virtual void Die()
    {

        if (!respawn || isObstacle)
        {
            Destroy(gameObject);
        }
        else
        {
            Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
            foreach (Collider2D col in colliders)
            {
                col.enabled = false;
            }
            sRenderer.enabled = false;
            StartCoroutine(RespawnEnemy());
        }

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
    private IEnumerator RespawnEnemy()
    {
        yield return new WaitForSeconds(respawnTime);

        if (respawnEnemyPrefab != null)
        {
           GameObject newEnemy = Instantiate(respawnEnemyPrefab, transform.position, transform.rotation);
            SpriteRenderer newEnemyRenderer = newEnemy.GetComponent<SpriteRenderer>();
            if (newEnemyRenderer != null)
            {
                newEnemyRenderer.enabled = true;
            }
            Collider2D newEnemyCollider = newEnemy.GetComponent<Collider2D>();
            if (newEnemyCollider != null)
            {
                newEnemyCollider.enabled = true; 
            }
        }
        else
        {
            Debug.LogError("Enemy prefab is not assigned!");
        }
        Destroy(gameObject);
    }
    public void EnableAttackCollider()
    {
    
            attackCollision = true;
        
    }
    public void DisableAttackCollider()
    {

            attackCollision = false;
        
    }
    protected void PlaySound(string soundName)
    {
        if (!SoundManager.Instance.IsEnemySoundPlaying())
        {
            SoundManager.Instance.StopEnemySound2D();
            SoundManager.Instance.PlayEnemySound(soundName);
        }
    }
    public abstract void Attack();
    public abstract void StopAttack();
}

