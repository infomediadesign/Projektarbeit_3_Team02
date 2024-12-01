using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int damage;

    public void Attack(EnemyBase enemy)
    {
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        else
        {
            Debug.LogWarning("Kein Enemy getroffen");
        }
    }
}
