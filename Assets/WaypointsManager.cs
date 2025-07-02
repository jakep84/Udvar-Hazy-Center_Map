using UnityEngine;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour
{
    public Transform user;
    public GameObject linePrefab; // dashed line using LineRenderer

    private List<Waypoint> allWaypoints = new List<Waypoint>();
    private List<GameObject> activeLines = new List<GameObject>();

    void Start()
    {
        allWaypoints.AddRange(FindObjectsOfType<Waypoint>());
    }

    public void HighlightWaypoints(string type)
    {
        ClearLines();

        foreach (var wp in allWaypoints)
        {
            if (wp.type == type)
            {
                wp.Show();
                DrawDashedLine(user.position, wp.transform.position);
            }
            else
            {
                wp.Hide();
            }
        }
    }

    void DrawDashedLine(Vector3 start, Vector3 end)
    {
        GameObject lineObj = Instantiate(linePrefab);
        LineRenderer lr = lineObj.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        activeLines.Add(lineObj);
    }

    void ClearLines()
    {
        foreach (var line in activeLines)
            Destroy(line);
        activeLines.Clear();
    }
}
