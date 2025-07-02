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
    }

    public void Hide()
    {
        GetComponent<Renderer>().material = hiddenMaterial;
        isActive = false;
    }
}
