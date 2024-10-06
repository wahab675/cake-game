using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public Vector3 rotationDirection = Vector3.up;

    void Update()
    {
        // Rotate the GameObject based on the specified direction and speed
        transform.Rotate(rotationDirection * rotationSpeed * Time.deltaTime);
    }
}
