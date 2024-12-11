using UnityEngine;

public abstract class Collectibles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollect(collision.GetComponent<Player>());
    }

    public abstract void OnCollect(Player player);
}
