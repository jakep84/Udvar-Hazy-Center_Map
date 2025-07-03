using UnityEngine;

public class UserMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(-h, 0, -v);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }
}
