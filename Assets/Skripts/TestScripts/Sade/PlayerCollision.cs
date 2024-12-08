using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public List<string> inventory;

    void Start()
    {
        inventory = new List<string>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Collectable"))
        {
           string itemType = collision.gameObject.GetComponent<Collectables>().itemType;
           
           print("we have collected a " + itemType);

           inventory.Add(itemType);
           print("Inventory lenght:" + inventory.Count);

           Destroy(collision.gameObject);

           SoundManager.Instance.PlaySound2D("Collectable"); //test
           
        }
    }

}
