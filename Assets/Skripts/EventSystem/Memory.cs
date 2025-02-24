using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Memory : MonoBehaviour
{
     public int memoryValue = 1; // Default value for memory

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EventManager.Instance.TriggerEvent("MemoryCollected", memoryValue); // Pass memoryValue as parameter
            SoundManager.Instance.PlaySound2D("Memory");
            Destroy(gameObject);
        }
    }
}
