using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public string type;  // Example: "ATM", "Restroom", "Food"
    public Material visibleMaterial;
    public Material hiddenMaterial;
    public bool isActive;

    public void Show()
    {
        GetComponent<Renderer>().material = visibleMaterial;
        isActive = true;
        Debug.Log($"Waypoint of type {type} is now visible.");
    }

    public void Hide()
    {
        GetComponent<Renderer>().material = hiddenMaterial;
        Debug.Log($"Waypoint of type {type} is now hidden.");
        isActive = false;
    }
}
