using UnityEngine;

public class MapDragZoomController : MonoBehaviour
{
    [Header("Drag Settings")]
    public float dragSpeed = 0.01f;
    private Vector3 lastTouchPosition;
    private bool isDragging = false;

    [Header("Zoom Settings")]
    public float zoomSpeed = 0.01f;
    public float minZoom = 0.5f;
    public float maxZoom = 3f;

    [Header("Map Bounds")]
    public Vector2 minPosition = new Vector2(-880f, -693f); // X, Z min
    public Vector2 maxPosition = new Vector2(605f, 466f);   // X, Z max


    void Update()
    {
#if UNITY_EDITOR
        HandleMouseDrag();
#else
        HandleTouch();
#endif
    }

    void HandleMouseDrag()
    {
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

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            ZoomMap(-scroll * 100); // invert scroll direction
        }
    }

    void HandleTouch()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                PanMap(touch.deltaPosition);
            }
        }
        else if (Input.touchCount == 2)
        {
            // Pinch-to-zoom
            Touch t1 = Input.GetTouch(0);
            Touch t2 = Input.GetTouch(1);

            Vector2 prevPos1 = t1.position - t1.deltaPosition;
            Vector2 prevPos2 = t2.position - t2.deltaPosition;

            float prevDistance = Vector2.Distance(prevPos1, prevPos2);
            float currDistance = Vector2.Distance(t1.position, t2.position);

            float delta = currDistance - prevDistance;
            ZoomMap(delta);
        }
    }

    void PanMap(Vector2 delta)
    {
        Vector3 move = new Vector3(-delta.x * dragSpeed, 0, -delta.y * dragSpeed);
        transform.Translate(move, Space.World);

        ClampMapPosition();
    }

    void ZoomMap(float delta)
    {
        float currentScale = transform.localScale.x;
        float newScale = Mathf.Clamp(currentScale + delta * zoomSpeed, minZoom, maxZoom);
        transform.localScale = Vector3.one * newScale;
    }

    void ClampMapPosition()
    {
        Vector3 pos = transform.position;

        // Adjust bounds based on current zoom scale
        float scale = transform.localScale.x;
        float xMin = minPosition.x * scale;
        float xMax = maxPosition.x * scale;
        float zMin = minPosition.y * scale;
        float zMax = maxPosition.y * scale;

        pos.x = Mathf.Clamp(pos.x, xMin, xMax);
        pos.z = Mathf.Clamp(pos.z, zMin, zMax);

        transform.position = pos;
    }
}
