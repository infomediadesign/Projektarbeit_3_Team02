using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EventManager.Instance.TriggerEvent("LifeCollected");
            SoundManager.Instance.PlaySound2D("Life");
            Destroy(gameObject);
        }
    }

}
