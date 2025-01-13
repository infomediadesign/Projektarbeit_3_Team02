using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
   public LifeUIText lifeUIText; //drag the UI Text script to the Inspector

    private void Start()
    {
        if (lifeUIText != null)
        {
            EventManager.Instance.StartListening("LifeCollected", lifeUIText.IncrementLifeCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EventManager.Instance.TriggerEvent("LifeCollected");
            SoundManager.Instance.PlaySound2D("Collectibles");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (lifeUIText != null)
        {
            EventManager.Instance.StopListening("LifeCollected", lifeUIText.IncrementLifeCount);
        }
    }
}
