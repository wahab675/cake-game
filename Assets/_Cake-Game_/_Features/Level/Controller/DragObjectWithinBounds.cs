using UnityEngine;
using System;
public class DragObjectWithinBounds : MonoBehaviour
{
    public Vector2 minBounds; // Minimum X and Y bounds
    public Vector2 maxBounds; // Maximum X and Y bounds

    private Vector3 offset;
    private Camera mainCamera;
    public bool isDragging = false;

    public Action OnDragBeginAction;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Check for mouse or touch input to start dragging
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging(Input.mousePosition);
        }
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                StartDragging(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                OnDrag(touch.position);
            }
        }

        // While dragging with mouse
        if (isDragging && Input.GetMouseButton(0))
        {
            OnDrag(Input.mousePosition);
        }

        // Stop dragging if mouse button is released or touch ends
        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            isDragging = false;
        }
    }

    void StartDragging(Vector3 inputPosition)
    {
        // Calculate the offset between the object's position and the mouse/touch position
        Vector3 objectPosition = GetWorldPosition(inputPosition);
        offset = transform.position - objectPosition;
        isDragging = true;
        OnDragBeginAction?.Invoke();
    }

    void OnDrag(Vector3 inputPosition)
    {
        // Convert the mouse/touch position to world space
        Vector3 worldPosition = GetWorldPosition(inputPosition);

        // Calculate the new position with the offset
        Vector3 newPosition = worldPosition + offset;

        // Clamp the position within bounds
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

        // Apply the clamped position to the object
        transform.localPosition = newPosition;
    }

    // Convert the screen position (mouse/touch) to world position
    Vector3 GetWorldPosition(Vector3 inputPosition)
    {
        inputPosition.z = Mathf.Abs(mainCamera.transform.position.z);  // Set the Z distance
        return mainCamera.ScreenToWorldPoint(inputPosition);  // Convert screen position to world space
    }
}
