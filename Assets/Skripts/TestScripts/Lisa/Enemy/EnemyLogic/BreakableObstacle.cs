using UnityEngine;

public class BreakableObstacle : EnemyBase
{
    public ObstacleStats ObstStats;
    public Boss boss; 
    public bool weakness;
    void Awake()
    {
        GameObject bossObject = GameObject.FindGameObjectWithTag("Boss");

        if (bossObject != null)
        {
            boss = bossObject.GetComponent<Boss>(); 
        }
    }

    void Update()
    {

    }
    public override void Attack()
    {
    }

    public override void StopAttack()
    {
    }

    void OnDestroy() 
    {
        if (weakness)
        {
            boss.OnWeaknessDestroyed();
        }
    }
}
