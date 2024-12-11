using UnityEngine;

public class Memories : Collectibles
{

    public override void OnCollect(Player player)
    {
        player.CollectMemories();
        Destroy(gameObject);
    }
}
