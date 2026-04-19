using UnityEngine;

public class Grap : MonoBehaviour
{

    public ElementalChecker elementChecker; 


    public float maxDistance = 15f;
    public float grappleSpeed = 20f;
    public LayerMask grappleLayer;

    private LineRenderer lineRenderer;
    private Rigidbody2D rb;

    private Vector2 grapplePoint;
    private bool isGrappling = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            TryStartGrapple();
        }
        if (Input.GetMouseButtonUp(1))
        {
            StopGrapple();
        }
        if (isGrappling)
        {
            DrawLine();
        }
    }

    void FixedUpdate()
    {
        if (isGrappling)
        {
            Vector2 dir = (grapplePoint - (Vector2)transform.position).normalized;
            rb.linearVelocity = dir * grappleSpeed;
        }
    }

    void TryStartGrapple()
    {
        if (!elementChecker.isNature)
        return;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 direction = (mousePos - playerPos).normalized;
        RaycastHit2D hit = Physics2D.Raycast(playerPos, direction, maxDistance);

        if (hit.collider != null)
        {
             if (!hit.collider.CompareTag("Grounded"))
             return;

             isGrappling = true;
             grapplePoint = hit.point;
             lineRenderer.positionCount = 2;

        }

    }

    void StopGrapple()
    {
        isGrappling = false;
        lineRenderer.positionCount = 0;
    }


    void DrawLine()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, grapplePoint);
    }
}
