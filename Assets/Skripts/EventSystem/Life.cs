using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    public int lifeValue = 10; // Amount of life to restore

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EventManager.Instance.TriggerEvent("LifeCollected", lifeValue); // Pass lifeValue as parameter
            SoundManager.Instance.PlaySound2D("Life");
            Destroy(gameObject);
        }
    }
}
