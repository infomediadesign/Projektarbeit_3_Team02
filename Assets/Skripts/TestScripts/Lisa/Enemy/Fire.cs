using UnityEngine;

public class Fire : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    private Vector2 direction;

    public void FireShot(Vector2 direction)
    {
        this.direction = direction;
    }

    void Update()
    {
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
