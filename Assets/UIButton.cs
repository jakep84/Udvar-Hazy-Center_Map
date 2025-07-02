using UnityEngine;

public class UIButton : MonoBehaviour
{
    public string targetType; // Set in Inspector per button
    public WaypointManager manager;

    public void OnClick()
    {
        manager.HighlightWaypoints(targetType);
    }
}
