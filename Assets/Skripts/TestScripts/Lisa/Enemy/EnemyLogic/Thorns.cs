using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    public float moveRange = 5f;
    public float moveSpeed = 2f;
    public float damage = 25f;
    private PlayerHealth playerHealth;
    public BoxCollider2D dmgHitbox;

    protected float damageCooldown = 1.5f; 
    protected float damageCooldownTimer = 0f;

    private Vector3 startPosition;
    private float direction = 1f; //positiv oder negatic´v

    void Start()
    {
        if (moveSpeed > 0 && moveRange > 0)
        {
            startPosition = transform.position;
        }
    }

    void Update()
    {
        if (moveSpeed > 0 && moveRange > 0)
        {
            float offset = Mathf.PingPong(Time.time * moveSpeed, moveRange * 2) - moveRange;
            transform.position = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.IsTouching(dmgHitbox))
        {
            Debug.Log("trigger");
            playerHealth = other.GetComponent<PlayerHealth>();

            playerHealth.TakeDamage(damage);
            Debug.Log("taking damage: " + damage);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.IsTouching(dmgHitbox))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            if (damageCooldownTimer <= 0f)
            {

                playerHealth.TakeDamage(damage);
                Debug.Log("taking damage: " + damage);

                damageCooldownTimer = damageCooldown;
            }
        }
    }
}