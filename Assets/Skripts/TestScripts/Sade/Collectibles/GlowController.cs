using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class GlowController : MonoBehaviour
{
    public Material GlowMaterial;
    public Material DefaultMaterial;

    private SpriteRenderer _spriteRenderer;
    private bool _isCollected = false; // Track if the collectible is collected

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("GlowController: SpriteRenderer component not found!");
            return;
        }

        _spriteRenderer.material = DefaultMaterial;
        Debug.Log("GlowController: Default material set.");
    }

    public void ToggleGlow(bool isGlowing)
{
    if (_isCollected) 
    {
        Debug.Log("GlowController: Skipping toggle due to collection.");
        return;
    }

    if (_spriteRenderer == null)
    {
        Debug.LogError("GlowController: SpriteRenderer not assigned!");
        return;
    }

    if (isGlowing)
    {
        Debug.Log("GlowController: Applying GlowMaterial.");
        _spriteRenderer.material = GlowMaterial;
    }
    else
    {
        Debug.Log("GlowController: Applying DefaultMaterial.");
        _spriteRenderer.material = DefaultMaterial;
    }
}

    public void MarkAsCollected()
    {
        _isCollected = true;
        Debug.Log("GlowController: Marked as collected. Glow changes disabled.");
    }
}