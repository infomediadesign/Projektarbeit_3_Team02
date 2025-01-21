using UnityEngine;

public class Fire : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    private Vector2 direction;
    public float followDuration = 2f;

    private bool isFollowing;
    private float followingTimer;
    public Transform playerTarget;
    public void FireShot(Vector2 direction)
    {
        this.direction = direction;
        isFollowing = false;
    }

    public void FireFollowing(Transform target)
    {
        playerTarget = target; //Spieler
        isFollowing = true;
        followingTimer = followDuration;
    }

    void Update()
    {
        if (isFollowing && playerTarget != null)
        {
            Vector2 targetDirection = (playerTarget.position - transform.position).normalized;
            direction = Vector2.Lerp(direction, targetDirection, Time.deltaTime * speed);
            followingTimer -= Time.deltaTime;

            if (followingTimer <= 0)
            {
                isFollowing = false; 
            }
        }

        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
