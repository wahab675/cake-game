using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public Vector3 moveDir = Vector3.forward; // Direction to move in
    public float moveDist = 10f; // Distance to move
    public float moveSpeed = 5f; // Speed of movement
    public float repeatDelay = 1f; // Delay before repeating

    private Vector3 startPos; // Starting position
    private float elapsedTime = 0f; // Time elapsed since last movement

    void Start()
    {
        startPos = transform.position; // Set the starting position
        elapsedTime = repeatDelay + 1;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime; // Update elapsed time

        if(elapsedTime >= repeatDelay)
        {
            // Calculate movement distance for this frame
            float step = moveSpeed * Time.deltaTime;

            // Move the object in the specified direction
            transform.Translate(moveDir.normalized * step);

            // If distance from start position is greater than moveDist, reset position
            if(Vector3.Distance(startPos, transform.position) >= moveDist)
            {
                transform.position = startPos; // Reset position
                elapsedTime = 0f; // Reset elapsed time
            }
        }
    }
}
