using UnityEngine;

public class DamageableAsset : EnemyBase
{
    private SpriteRenderer spriteRenderer; 
    public Sprite[] sprites; 
    public GameObject spawnOnDestroy;
    public Transform player;
    private bool hitcountUpdated = false;

    private int hitCount = 0;

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void Attack()
    {
    }
    public override void StopAttack()
    {
    }
    public override void TakeDamage(int damage)
    {
        if (!hitcountUpdated)
        {
            hitCount++;
            hitcountUpdated = true;
        }
       
        if (hitCount < 3)
        {
            UpdateAppearance();
        }
        else
        {
            DestroyAsset();
        }
    }

    private void UpdateAppearance()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites[hitCount];
        }
        hitcountUpdated = false;
    }

    private void DestroyAsset()
    {
        if (spawnOnDestroy != null && player != null)
        {
            Instantiate(spawnOnDestroy, player.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
