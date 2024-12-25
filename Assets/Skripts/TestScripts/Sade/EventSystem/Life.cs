using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
   public UnityEvent pickUpLife;

   private void Start()
   {
        pickUpLife.AddListener(GameObject.FindGameObjectWithTag("Life").GetComponent<LifeUIText>().IncrementLifeCount);
   }

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // ui text update number
            pickUpLife.Invoke();
            SoundManager.Instance.PlaySound2D("Collectibles");
            Destroy(gameObject);
            
            //collect
            //glow
        }
    }

    public void TestMethod()
    {
        print("test test test");
    }
}
