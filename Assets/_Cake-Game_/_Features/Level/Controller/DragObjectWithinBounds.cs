using UnityEngine;
using System;

public class DragObjectWithinBounds : MonoBehaviour
{
    // Enum to choose the drag mode
    public enum DragMode
    {
        DragOnObject,  // Dragging by clicking the object
        DragAnywhere   // Dragging from anywhere on the screen
    }

    public DragMode dragMode = DragMode.DragOnObject; // Drag mode selector in Inspector

    public Vector2 minBounds; // Minimum X and Y bounds
    public Vector2 maxBounds; // Maximum X and Y bounds

    private Vector3 offset;
    private Camera mainCamera;
    public bool isDragging = false;

    public Action OnDragBeginAction;
    public Action OnDraggingAction;
    public Func<Transform> ObjectToScaleOnDrag;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Check for drag initiation based on the selected drag mode
        if (dragMode == DragMode.DragOnObject)
        {
            DragOnObject();
        }
        else if (dragMode == DragMode.DragAnywhere)
        {
            DragAnywhere();
        }
    }

    void DragOnObject()
    {
        // Dragging only when clicking directly on the object
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the click/touch is on the object using a raycast
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                StartDragging(Input.mousePosition);
            }
        }

        // Handle dragging or touch events
        HandleDrag();
    }

    void DragAnywhere()
    {
        // Dragging from anywhere on the screen
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging(Input.mousePosition);
        }

        // Handle dragging or touch events
        HandleDrag();
    }

    private Vector3 initialDragPosition;
    private Vector3 initialScale;
    public float scaleFactor = 0.01f;
    void ScaleObject(Vector3 inputPosition, Transform targetObjectToScale)
    {
        if(targetObjectToScale.localScale == Vector3.one)
        {
            return;
        }
        // Get the current drag position
        Vector3 currentDragPosition = GetWorldPosition(inputPosition);

        // Calculate the difference between the initial and current drag positions
        float dragDistance = (currentDragPosition - initialDragPosition).magnitude;

        // Scale the object based on the drag distance and the scale factor
        float scaleAmount = 1 + dragDistance * scaleFactor;

        // Apply the new scale to the object
        targetObjectToScale.localScale = initialScale * scaleAmount;
    }
    void HandleDrag()
    {
        if (Input.touchCount > 0)
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

        if (isDragging && Input.GetMouseButton(0))
        {
            OnDrag(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            isDragging = false;
        }
    }

    void StartDragging(Vector3 inputPosition)
    {
        // Calculate the offset between the object's position and the mouse/touch position
        Vector3 objectPosition = GetWorldPosition(inputPosition);
        initialDragPosition = GetWorldPosition(inputPosition);
        if(ObjectToScaleOnDrag != null)
        {
            initialScale = ObjectToScaleOnDrag().localScale;
        }
      
        // Ensure the Z component is properly handled to avoid depth issues
        offset = transform.position - objectPosition;
        offset.z = 0;  // Ensure no Z-axis shift occurs when dragging

        isDragging = true;
        OnDragBeginAction?.Invoke();
    }

    void OnDrag(Vector3 inputPosition)
    {
        // Convert the mouse/touch position to world space
        Vector3 worldPosition = GetWorldPosition(inputPosition);

        if (ObjectToScaleOnDrag != null)
        {

        ScaleObject(initialDragPosition,ObjectToScaleOnDrag());
        }
        // Adjust the object's Z to maintain its original Z position
        worldPosition.z = transform.position.z;

        // Calculate the new position with the offset
        Vector3 newPosition = worldPosition + offset;

        // Clamp the position within bounds
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

        // Apply the clamped position to the object
        transform.position = newPosition;
        OnDraggingAction?.Invoke();
    }

    Vector3 GetWorldPosition(Vector3 inputPosition)
    {
        inputPosition.z = Mathf.Abs(mainCamera.transform.position.z);
        return mainCamera.ScreenToWorldPoint(inputPosition);
    }
}
