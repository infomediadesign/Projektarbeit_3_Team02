using UnityEngine;

public class ParallaxL2 : MonoBehaviour
{
    [HideInInspector] public Camera mainCamera;
    private float length, startpos;
    public float parallaxEffect;

    // Neuer Parameter für den X-Schwellenwert
    public float visibilityCutoffX = 0f;

    // Referenz auf den Sprite Renderer
    private SpriteRenderer spriteRenderer;

    // Material für den Cutoff-Effekt
    private Material cutoffMaterial;

    void Start()
    {
        // Finde die Kamera
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera")?.GetComponent<Camera>();
        if (mainCamera == null)
        {
            Debug.LogWarning("Could not find camera for parallax effect on: " + gameObject.name);
            return;
        }

        // Sprite Renderer holen
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("No SpriteRenderer found on: " + gameObject.name);
            return;
        }

        startpos = transform.position.x;
        length = spriteRenderer.bounds.size.x;

        // Erzeuge ein Material mit dem Standard Sprite Shader
        cutoffMaterial = new Material(Shader.Find("Sprites/Default"));

        // Kopiere die Properties vom Original-Material
        if (spriteRenderer.material != null)
        {
            cutoffMaterial.CopyPropertiesFromMaterial(spriteRenderer.material);
        }

        // Setze das Material als Instanz, damit wir es zur Laufzeit verändern können
        spriteRenderer.material = cutoffMaterial;
    }

    void FixedUpdate()
    {
        if (mainCamera == null) return;  // Sicherheitscheck

        float temp = (mainCamera.transform.position.x * (1 - parallaxEffect));
        float dist = (mainCamera.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        /*
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
        */

        // Implementiere die Cutoff-Logik
        UpdateCutoffVisibility();
    }

    void UpdateCutoffVisibility()
    {
        // Erstelle eine Maske für den Sprite, indem wir ihm ein eigenes Material geben
        if (spriteRenderer == null || cutoffMaterial == null) return;

        // Berechne die Weltkoordinaten des Sprites
        Bounds bounds = spriteRenderer.bounds;
        float spriteLeftEdge = bounds.min.x;
        float spriteRightEdge = bounds.max.x;
        float spriteWidth = bounds.size.x;

        // Berechne wie viel vom Sprite sichtbar sein soll (auf Basis des Cutoff-Werts)
        float visibleRatio = 0;

        if (spriteRightEdge <= visibilityCutoffX)
        {
            // Sprite ist komplett links vom Cutoff - komplett unsichtbar
            visibleRatio = 0;
        }
        else if (spriteLeftEdge >= visibilityCutoffX)
        {
            // Sprite ist komplett rechts vom Cutoff - komplett sichtbar
            visibleRatio = 1;
        }
        else
        {
            // Sprite überlappt den Cutoff - teilweise sichtbar
            float visiblePart = spriteRightEdge - visibilityCutoffX;
            visibleRatio = visiblePart / spriteWidth;
        }

        // Setze die Transparenz entsprechend
        Color color = spriteRenderer.color;

        // Special Case: Kompletter Fade
        if (visibleRatio <= 0)
        {
            // Komplett unsichtbar
            color.a = 0;
        }
        else if (visibleRatio >= 1)
        {
            // Komplett sichtbar
            color.a = 1;
        }
        else
        {
            // Berechne einen weichen Übergang basierend auf der Schnittlinie
            float fadeWidth = 0.2f; // 20% des Sprites für den Übergang
            float fadeStart = visibilityCutoffX;
            float fadeEnd = fadeStart + fadeWidth * spriteWidth;

            // Wenn wir innerhalb der Fade-Zone sind, berechne eine Überblendung
            if (spriteLeftEdge < fadeEnd && spriteRightEdge > fadeStart)
            {
                float fadeProgress = (spriteRightEdge - fadeStart) / (fadeEnd - fadeStart);
                fadeProgress = Mathf.Clamp01(fadeProgress);
                color.a = fadeProgress;
            }
        }

        // Wende die Farbe an
        spriteRenderer.color = color;
    }

    // Damit wir den Cutoff im Editor anpassen können
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(visibilityCutoffX, transform.position.y - 10, 0),
                        new Vector3(visibilityCutoffX, transform.position.y + 10, 0));
    }
}