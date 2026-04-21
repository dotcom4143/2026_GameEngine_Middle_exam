using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Transform checkPoint;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    public bool isGrounded { get; private set; }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(
            checkPoint.position,
            checkRadius,
            groundLayer
        );
    }

    void OnDrawGizmos()
    {
        if (checkPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkPoint.position, checkRadius);
    }
}
