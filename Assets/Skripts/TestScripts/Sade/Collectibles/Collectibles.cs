using UnityEngine;

public abstract class Collectibles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                OnPlayerEnter();        //trigger for child class specific behavior
                OnCollect(player);      //handles collection (Script Player)
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                OnPlayerExit();     //trigger for child class specific behavior when player leaves
                //OnCollect(player);
            }
        }
    }
    //methods for child class behavior 
    public abstract void OnCollect(Player player);
    protected virtual void OnPlayerEnter() {}
    protected virtual void OnPlayerExit() {}
}


/*using UnityEngine;

public abstract class Collectibles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollect(collision.GetComponent<Player>());
    }
    
    public abstract void OnCollect(Player player);
}*/