using UnityEngine;

public class Memories : Collectibles
{
    private GlowController _glowController;
    private bool _isCollected = false; // Track if memory is collected

   protected override void OnPlayerEnter()
{
    if (_isCollected) return; // Skip if already collected

    if (_glowController != null)
    {
        _glowController.ToggleGlow(true);
    }
}

protected override void OnPlayerExit()
{
    if (_isCollected) return; // Skip if already collected

    if (_glowController != null)
    {
        _glowController.ToggleGlow(false);
    }
}

public override void OnCollect(Player player)
{
    Debug.Log("OnCollect: Memory collected by player.");

    player.CollectMemories();
    SoundManager.Instance.PlaySound2D("Collectibles");

    if (_glowController != null)
    {
        Debug.Log("OnCollect: Forcing GlowMaterial for debugging.");
        _glowController.MarkAsCollected();
        _glowController.ToggleGlow(true);

        // Delay the destruction significantly for testing purposes
        Destroy(_glowController.gameObject, 5f); // Destroy glow after 5 seconds
    }

    Destroy(gameObject); // Destroy memory object
}
}



/*using UnityEngine;

public class Memories : Collectibles
{
    private GlowController _glowController;

    private void Awake()
    {
        _glowController = GetComponent<GlowController>();       //gets component in children
    }

    public override void OnCollect(Player player)
    {
        player.CollectMemories();
        SoundManager.Instance.PlaySound2D("Collectibles");
        Destroy(gameObject);
    }
}*/