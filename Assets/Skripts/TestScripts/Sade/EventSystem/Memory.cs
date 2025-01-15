using UnityEngine;
using UnityEngine.Events;

public class Memory : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EventManager.Instance.TriggerEvent("MemoryCollected");
            SoundManager.Instance.PlaySound2D("Collectibles");
            Destroy(gameObject);
        }
    }
}
