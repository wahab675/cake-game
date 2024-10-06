using UnityEngine;

public class RandomRotateZ : MonoBehaviour
{
    public float minAngle = -10f;
    public float maxAngle = 10f;

    void Start()
    {
        // Generate a random rotation angle within the specified range
        float randomAngle = Random.Range(minAngle, maxAngle);

        // Apply the rotation to the GameObject along the Z-axis
        transform.Rotate(0f, 0f, randomAngle);
    }
}
