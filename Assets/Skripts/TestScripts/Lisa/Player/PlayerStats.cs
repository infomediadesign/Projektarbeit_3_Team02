using UnityEngine;

[CreateAssetMenu(menuName = "Player/Stats")]
public class PlayerStats : ScriptableObject
{
    public float walkingSpeed = 5;
    public float jumpForce = 15;
    public float rollDistance = 5;
    public float rollSpeed = 8;
    public float groundCheckRad = 0.2f;
    public float enemyCheckRad = 0.2f;
    public float counterCheckRad = 0.5f;
    public float jumpMultiplier = 0.5f;
    public float airCounterForce = 15;
    public float airCounterMultiplier = 0.5f;
}
