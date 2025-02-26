using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

public class BreakableObstacle : EnemyBase
{
    public ObstacleStats obstStats;
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

    public override void Attack()
    {
    }
    public override void StopAttack()
    {
    }
    void Update()
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
