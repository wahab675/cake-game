using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public Sprite[] spriteArray; // Array of sprites to cycle through
    public float changeInterval = 0.2f; // Time between sprite changes
    public bool loop = true; // Toggle for looping the sprite change

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private int currentSpriteIndex = 0; // Current index of the sprite in the array
    private float timer = 0f; // Timer to track the time between changes

    void Start()
    {
        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the initial sprite
        if (spriteArray.Length > 0)
        {
            spriteRenderer.sprite = spriteArray[currentSpriteIndex];
        }
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // If the timer exceeds the change interval, update the sprite
        if (timer >= changeInterval)
        {
            // Reset the timer
            timer = 0f;

            // Increment the sprite index
            currentSpriteIndex++;

            // If we've reached the end of the array, either loop or stop
            if (currentSpriteIndex >= spriteArray.Length)
            {
                if (loop)
                {
                    // Loop back to the first sprite
                    currentSpriteIndex = 0;
                }
                else
                {
                    // Stop at the last sprite
                    currentSpriteIndex = spriteArray.Length - 1;
                }
            }

            // Change the sprite
            spriteRenderer.sprite = spriteArray[currentSpriteIndex];
        }
    }
}
