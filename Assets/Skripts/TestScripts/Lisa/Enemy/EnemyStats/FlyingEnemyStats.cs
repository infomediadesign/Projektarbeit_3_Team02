using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/FlyingStats")]
public class FlyingEnemyStats : EnemyStats
{
    public float flySpeed;
    public float shootingRange = 10f;
    public float shootCooldown = 2f;

}
