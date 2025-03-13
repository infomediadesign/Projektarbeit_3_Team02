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
    public void AttackThorn(DamageableAsset dmgAsset)
    {
        if (dmgAsset != null)
        {
            dmgAsset.TakeThornDamage();
        }
        else
        {
            Debug.LogWarning("Kein Enemy getroffen");
        }
    }
}
