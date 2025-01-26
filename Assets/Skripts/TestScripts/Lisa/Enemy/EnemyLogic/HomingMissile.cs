using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb;
    public int damage = 10;
    public float speed = 5f;
    public float rotateSpeed = 200f;

    public float followDuration = 3f;
    private bool isFollowing = true;
    private float followTimer;
    private Vector2 currentDirection;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        followTimer = followDuration;
        currentDirection = transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            followTimer -= Time.deltaTime;

            if (followTimer <= 0f)
            {
                isFollowing = false; // timer abgelaufen, verfolgen beenden
                currentDirection = transform.up;
                rb.angularVelocity = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (isFollowing)
        {
            Vector2 direction = ((Vector2)target.position - rb.position).normalized;
            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed; // drehen in zielrcihtung
            rb.velocity = transform.up * speed;
            currentDirection = rb.linearVelocity.normalized;
        }
        else
        {
            rb.velocity = currentDirection * speed;
        }
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
