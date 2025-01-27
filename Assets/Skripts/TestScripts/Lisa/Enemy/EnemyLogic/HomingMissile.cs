using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    public int damage = 10;
    public float speed = 5f;
    public float rotateSpeed = 200f;

    public float followDuration = 3f;
    private bool isFollowing = true;
    private float followTimer;
    private Vector2 currentDirection;
    private float input;
    void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
        //target = GameObject.FindGameObjectWithTag("Player").transform;
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
                rb.angularVelocity = 0;
            }
        }
        input = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        /*if (isFollowing)
        {
            Vector2 direction = ((Vector2)player.position - rb.position).normalized;
            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed; // drehen in zielrcihtung
            rb.linearVelocity = transform.up * speed;
            currentDirection = rb.linearVelocity.normalized;
        }
        else
        {
            rb.linearVelocity = currentDirection * speed;
        }*/
        if (isFollowing)
        {
            rb.linearVelocity = transform.up * speed * Time.deltaTime * 10f;
            Vector2 direction = (player.transform.position - transform.position).normalized;
            float rotationSteer = Vector3.Cross(transform.up, direction).z;
            rb.angularVelocity = rotationSteer * rotateSpeed * 10f;
            currentDirection = direction;
        }
        else
        {
            rb.linearVelocity = transform.up * speed * Time.deltaTime * 10f;
            float rotationSteer = Vector3.Cross(transform.up, currentDirection).z;
            rb.angularVelocity = rotationSteer * rotateSpeed * 10f;
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
