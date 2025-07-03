using UnityEngine;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour
{
    public Transform user;
    public GameObject linePrefab;

    private List<Waypoint> allWaypoints = new List<Waypoint>();
    private List<LineData> activeLines = new List<LineData>();

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

        foreach (var wp in allWaypoints)
            wp?.Hide();

        ClearLines();
    }

    void Update()
    {
        // Dynamically update line length/positions
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

        foreach (var wp in allWaypoints)
        {
            if (wp == null) continue;

            if (wp.type.Trim().ToLower() == type.Trim().ToLower())
            {
                wp.Show();
                CreateDynamicLineTo(wp.transform);
            }
            else
            {
                wp.Hide();
            }
        }
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
}
