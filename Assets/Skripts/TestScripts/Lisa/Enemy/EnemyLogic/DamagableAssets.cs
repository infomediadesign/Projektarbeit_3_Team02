using UnityEngine;

public class DamageableAsset : EnemyBase
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public GameObject spawnOnDestroy;
    public Transform player;

    private int hitCount = 0; // Zählt Treffer

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
    public override void StopAttack() { }

    public void TakeThornDamage()
    {

        if (hitCount < sprites.Length)
        {
            UpdateAppearance();
        }
        else
        {
            DestroyAsset();
        }

        hitCount++; // Erhöhe erst nach der Prüfung!

    }

    private void UpdateAppearance()
    {
        if (spriteRenderer != null && hitCount < sprites.Length)
        {
            spriteRenderer.sprite = sprites[hitCount];
        }
    }

    private void DestroyAsset()
    {
        Debug.Log("thorn destroyed!");
        if (spawnOnDestroy != null && player != null)
        {
            Instantiate(spawnOnDestroy, player.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
