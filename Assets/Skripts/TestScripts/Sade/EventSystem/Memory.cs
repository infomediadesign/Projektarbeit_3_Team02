using UnityEngine;
using UnityEngine.Events;

public class Memory : MonoBehaviour
{
    public UnityEvent pickUpMemory;

    private void Start()
    {
         pickUpMemory.AddListener(GameObject.FindGameObjectWithTag("Memory").GetComponent<MemoryUIText>().IncrementMemoryCount);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // ui text update number
            pickUpMemory.Invoke();
            SoundManager.Instance.PlaySound2D("Collectibles");
            Destroy(gameObject);
            
            //collect
            //glow
        }
    }
}
