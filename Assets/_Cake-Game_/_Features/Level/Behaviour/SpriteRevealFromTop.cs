using UnityEngine;

public class SpriteRevealFromTop : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // The SpriteRenderer component to manipulate
    public float revealDuration = 2f; // Time taken to fully reveal the sprite

    private Material revealMaterial; // Material instance for the effect
    private float revealProgress = 0f; // Progress of the reveal effect

    private static readonly int CutoffID = Shader.PropertyToID("_Cutoff"); // Cached property ID for _Cutoff

    void Start()
    {
        // Use the material instance directly instead of instantiating a new one
        // Ensure the material is already assigned via the inspector
        if (spriteRenderer != null)
        {
            revealMaterial = spriteRenderer.material;

            // Set the initial reveal value (0 = fully hidden)
            revealMaterial.SetFloat(CutoffID, 1.0f);
        }
        else
        {
            Debug.LogError("SpriteRenderer not assigned.");
        }
    }

    void Update()
    {
        if (revealMaterial == null) return;

        // Increase reveal progress over time
        revealProgress += Time.deltaTime / revealDuration;

        // Clamp revealProgress between 0 and 1
        revealProgress = Mathf.Clamp01(revealProgress);

        // Update the material to reveal the sprite from top to bottom
        revealMaterial.SetFloat(CutoffID, 1.0f - revealProgress);
    }
}
