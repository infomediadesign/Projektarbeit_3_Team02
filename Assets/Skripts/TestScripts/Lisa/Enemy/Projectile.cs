using UnityEngine;

public class Projectile : EnemyBase
{
    PlayerHealth playerHealth;
    public ProjectileStats p_stats;
    public LayerMask groundLayer;
    void Update()
    {
        transform.Translate(Vector2.down * p_stats.fallSpeed * Time.deltaTime);
        if (IsGrounded())
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(stats.damage);
        }
    }
    public override void Attack()
    {

    }

    public override void StopAttack()
    {

    }
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);
    }
}
