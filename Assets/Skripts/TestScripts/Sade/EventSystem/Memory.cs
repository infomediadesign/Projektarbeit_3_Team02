using UnityEngine;
using UnityEngine.Events;

public class Memory : MonoBehaviour
{
    public UnityEvent pickUpMemory;

    private void Start()
    {
        //update UI text
        pickUpMemory.AddListener(GameObject.FindGameObjectWithTag("Memory").GetComponent<MemoryUIText>().IncrementMemoryCount);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            pickUpMemory.Invoke();
            SoundManager.Instance.PlaySound2D("Collectibles");      //play sound on pickup
            //object glows
            Destroy(gameObject);
        }
    }
}
