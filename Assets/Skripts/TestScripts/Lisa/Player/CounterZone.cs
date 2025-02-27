using System.Collections.Generic;
using UnityEngine;

public class CounterZone : MonoBehaviour
{
    private List<EnemyBase> enemiesInRange = new List<EnemyBase>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemiesInRange.Remove(enemy);
        }
    }

    public EnemyBase GetCounterableEnemy()
    {
        foreach (EnemyBase enemy in enemiesInRange)
        {
            if (enemy.GetCounterPossible())
            {
                return enemy;
            }
            else if (enemy.isObstacle)
            {
                return enemy;
            }
        }
        return null;
    }
}
