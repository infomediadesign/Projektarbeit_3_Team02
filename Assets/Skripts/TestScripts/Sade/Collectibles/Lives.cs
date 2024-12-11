using UnityEngine;

public class Lives : Collectibles
{
    [SerializeField] float healAmount = 1f;       //able to set in inspector
    public override void OnCollect(Player player)
    {
        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount); 
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("No PlayerHealth component on player object");
            }
        }
    }
}
