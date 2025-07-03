using UnityEngine;

public class UIButton : MonoBehaviour
{
    public string targetType; 
    public WaypointManager manager;

    public void OnClick()
    {
        Debug.Log($"Button clicked for target type: {targetType}");
        if (manager == null)
        {
            Debug.LogError("WaypointManager is not assigned!");
            return;
        }
        manager.HighlightWaypoints(targetType);
    }
}
