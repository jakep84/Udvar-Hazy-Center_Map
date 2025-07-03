using UnityEngine;

public class MapDragController : MonoBehaviour
{
    public float dragSpeed = 0.01f;

    private Vector3 lastTouchPosition;
    private bool isDragging = false;

    void Update()
    {
#if UNITY_EDITOR
        // Mouse drag support for editor
        if (Input.GetMouseButtonDown(0))
        {
            lastTouchPosition = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 delta = Input.mousePosition - lastTouchPosition;
            PanMap(delta);
            lastTouchPosition = Input.mousePosition;
        }

#else
        // Touch drag support for mobile
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                PanMap(touch.deltaPosition);
            }
        }
#endif
    }

    private void PanMap(Vector2 delta)
    {
        // Invert delta so dragging left moves map right, etc.
        Vector3 move = new Vector3(-delta.x * dragSpeed, 0, -delta.y * dragSpeed);
        transform.Translate(move, Space.World);
    }
}
