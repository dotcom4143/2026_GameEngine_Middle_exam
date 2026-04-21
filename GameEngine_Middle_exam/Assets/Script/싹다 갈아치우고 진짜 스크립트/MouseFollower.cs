using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    public float zDistance = 10f;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = zDistance;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        transform.position = worldPos;
    }
}