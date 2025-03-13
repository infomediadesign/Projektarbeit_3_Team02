using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

public class BreakableObstacle : EnemyBase
{
    public ObstacleStats obstStats;
    public Boss boss; 
    public bool weakness;
    public bool thornLevel2;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public GameObject spawnOnDestroy;
    public Transform player;
    void Awake()
    {
        if (weakness)
        {
            GameObject bossObject = GameObject.FindGameObjectWithTag("Boss");

            if (bossObject != null)
            {
                boss = bossObject.GetComponent<Boss>();
            }
        }
        if (thornLevel2)
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

    }

    public override void Attack()
    {
    }
    public override void StopAttack()
    {
    }
    void Update()
    {
        if (thornLevel2)
        {
            if(currentHealth == 3)
            {
                spriteRenderer.sprite = sprites[0];
            }
            else if (currentHealth == 2)
            {
                spriteRenderer.sprite = sprites[1];
            }
            else if (currentHealth == 1)
            {
                spriteRenderer.sprite = sprites[2];
            }
        }
    }

    void OnDestroy() 
    {
        if (weakness)
        {
            boss.OnWeaknessDestroyed();

        }
        if (thornLevel2)
        {
            if (spawnOnDestroy != null && player != null)
            {
                Instantiate(spawnOnDestroy, player.position, Quaternion.identity);
            }
        }
    }
}
