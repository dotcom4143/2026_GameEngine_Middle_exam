using UnityEngine;

public class BlinkToHere : MonoBehaviour
{
    public Transform player;
    public Transform mouseFollower;
    public float distanceFromPlayer = 3f;

    private ElementalChecker elementalChecker;

    void Start()
    {
        elementalChecker = player.GetComponent<ElementalChecker>();
    }

    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        Vector2 dir = (mouseFollower.position - player.position).normalized;
        transform.position = (Vector2)player.position + dir * distanceFromPlayer;
        transform.right = dir;
    }
}