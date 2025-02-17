using System.Xml.Serialization;
using UnityEngine;

public class Projectile : EnemyBase
{
    PlayerHealth playerHealth;
    public ProjectileStats p_stats;
    public LayerMask groundLayer;
    private bool isDestroyed = false;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (!isDestroyed)
        {
            transform.Translate(Vector2.down * p_stats.fallSpeed * Time.deltaTime);
        }
       
        if (IsGrounded() && !isDestroyed)
        {
            StartDestroySequence();
        }
        if (isDying)
        {
            DestroyObject();
        }
    }
    private void StartDestroySequence()
    {
        isDestroyed = true;
        anim.SetBool("destroy",true);
        anim.Play("ProjectileDestroyAnim", 0, 0f);

        float destroyDelay = anim.GetCurrentAnimatorStateInfo(0).length - 0.2f; //muss solange wie animation gehen, dann destroy
        Invoke(nameof(DestroyObject), destroyDelay);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    public override void Attack()
    {

    }

    public override void StopAttack()
    {

    }
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position, 0.6f, groundLayer);
    }
}
