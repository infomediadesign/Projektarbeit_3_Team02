using UnityEngine;

public class Memories : Collectibles
{
    public override void OnCollect(Player player)
    {
        player.CollectMemories();
        SoundManager.Instance.PlaySound2D("Collectibles");
        Destroy(gameObject);
    }
}
