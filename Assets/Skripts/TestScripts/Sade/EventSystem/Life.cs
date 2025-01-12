using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
   public UnityEvent pickUpLife;

   private void Start()
   {
        //update UI text 
        pickUpLife.AddListener(GameObject.FindGameObjectWithTag("Life").GetComponent<LifeUIText>().IncrementLifeCount);
   }

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            pickUpLife.Invoke();
            SoundManager.Instance.PlaySound2D("Collectibles");
            //add player health 
            Destroy(gameObject);
        }
    }
}
