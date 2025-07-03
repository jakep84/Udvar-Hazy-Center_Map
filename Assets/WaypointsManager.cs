using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour
{
    public Transform user;
    public GameObject linePrefab;
    public Camera mainCamera;
    public float padding = 3f;
    public float zoomSpeed = 3f;
    public bool useOrthographicZoom = true;
    

    private List<Waypoint> allWaypoints = new List<Waypoint>();
    private List<LineData> activeLines = new List<LineData>();
    private Transform closestTarget;

    private Vector3 defaultCamPos;
    private float defaultOrthoSize;


    private class LineData
    {
        public LineRenderer line;
        public Transform target;
    }

    void Start()
    {
        allWaypoints.AddRange(FindObjectsOfType<Waypoint>());

        if (user == null)
            Debug.LogError("User Transform not assigned!");

        if (linePrefab == null)
            Debug.LogError("Line Prefab not assigned!");

        if (mainCamera == null)
            mainCamera = Camera.main;

        defaultCamPos = mainCamera.transform.position;
        defaultOrthoSize = mainCamera.orthographicSize;

        foreach (var wp in allWaypoints)
            wp?.Hide();

        ClearLines();
    }


    void Update()
    {

        foreach (var lineData in activeLines)
        {
            if (lineData.line == null || lineData.target == null) continue;

            Vector3 start = user.position;
            Vector3 end = lineData.target.position;

            lineData.line.SetPosition(0, start);
            lineData.line.SetPosition(1, end);

            float distance = Vector3.Distance(start, end);
            float width = Mathf.Clamp(0.05f + distance * 0.01f, 0.05f, 0.5f);
            lineData.line.startWidth = width;
            lineData.line.endWidth = width;
        }
    }

    public void HighlightWaypoints(string type)
    {
        ClearLines();
        closestTarget = null;

        foreach (var wp in allWaypoints)
        {
            if (wp == null) continue;

            if (wp.type.Trim().ToLower() == type.Trim().ToLower())
            {
                wp.Show();
                CreateDynamicLineTo(wp.transform);

                if (closestTarget == null ||
                    Vector3.Distance(user.position, wp.transform.position) <
                    Vector3.Distance(user.position, closestTarget.position))
                {
                    closestTarget = wp.transform;
                }
            }
            else
            {
                wp.Hide();
            }
        }

        if (closestTarget != null)
            AdjustCameraToUserAndTarget(closestTarget);
    }

    void CreateDynamicLineTo(Transform target)
    {
        GameObject lineObj = Instantiate(linePrefab);
        LineRenderer lr = lineObj.GetComponent<LineRenderer>();

        if (lr == null)
        {
            Debug.LogError("Line prefab missing LineRenderer!");
            Destroy(lineObj);
            return;
        }

        lr.positionCount = 2;
        lr.useWorldSpace = true;

        LineData data = new LineData
        {
            line = lr,
            target = target
        };

        activeLines.Add(data);
    }

    void ClearLines()
    {
        foreach (var lineData in activeLines)
        {
            if (lineData.line != null)
                Destroy(lineData.line.gameObject);
        }
        activeLines.Clear();
    }

    IEnumerator SmoothCameraMove(Vector3 targetPos)
    {
        Vector3 start = mainCamera.transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * zoomSpeed;
            mainCamera.transform.position = Vector3.Lerp(start, targetPos, t);
            yield return null;
        }
    }



    void AdjustCameraToUserAndTarget(Transform target)
    {
        if (mainCamera == null || user == null || target == null) return;

        Vector3 userPos = user.position;
        Vector3 targetPos = target.position;

        Vector3 centerPoint = (userPos + targetPos) / 2f;

        // Keep camera height fixed (Y), only move in XZ plane
        Vector3 newCamPos = new Vector3(centerPoint.x, mainCamera.transform.position.y, centerPoint.z);

        // Calculate distance in XZ plane
        float flatDistance = Vector2.Distance(
            new Vector2(userPos.x, userPos.z),
            new Vector2(targetPos.x, targetPos.z)
        );

        float targetSize = flatDistance / 2f + padding;

        StartCoroutine(SmoothCameraMove(newCamPos));
        StartCoroutine(SmoothOrthoZoom(targetSize));
    }




    IEnumerator SmoothOrthoZoom(float targetSize)
    {
        float startSize = mainCamera.orthographicSize;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * zoomSpeed;
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            yield return null;
        }
    }

    public void ResetCamera()
    {
        StartCoroutine(SmoothCameraMove(defaultCamPos));
        StartCoroutine(SmoothOrthoZoom(defaultOrthoSize));
    }

}
