using UnityEngine;
using UnityEngine.InputSystem;

public class RealPlayerController : MonoBehaviour
{
    public GameObject hookPrefab;
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public Camera cam;
    public ElementalChecker elementChecker;

    public float grappleForce = 15f;
    public float maxGrappleDistance = 8f;

    private bool isGrappling = false;
    private Vector2 grapplePoint;

    public float moveSpeed = 5f;
    private float moveInput;

    public float jumpForce = 7f;
    public bool isGrounded;

    private Rigidbody2D rb;
    private Animator pAni;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();

        if (lineRenderer != null)
            lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (elementChecker != null && elementChecker.CanGrapple())
            {
                FireHook();
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            StopGrapple();
        }

        if (isGrappling && lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }
    }

    void FixedUpdate()
    {
        Move();

        if (isGrappling)
        {
            Vector2 dir = (grapplePoint - (Vector2)transform.position);
            float distance = dir.magnitude;

            dir.Normalize();

            float forceMultiplier = Mathf.Clamp(distance / maxGrappleDistance, 0.5f, 1.5f);

            rb.AddForce(dir * grappleForce * forceMultiplier, ForceMode2D.Force);

            if (distance < 1.0f)
            {
                StopGrapple();
            }

            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, 15f);
        }
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().x;

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            if (pAni != null)
                pAni.SetTrigger("Jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grounded"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grounded"))
            isGrounded = false;
    }

    void FireHook()
    {
        if (hookPrefab == null || firePoint == null || cam == null)
            return;

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)firePoint.position).normalized;

        GameObject hook = Instantiate(hookPrefab, firePoint.position, Quaternion.identity);

        GrappleHook gh = hook.GetComponent<GrappleHook>();

        if (gh != null)
        {
            gh.Init(dir, this);
        }
    }

    public void OnHookHit(Vector2 point)
    {
        grapplePoint = point;
        isGrappling = true;
    }

    public void StopGrapple()
    {
        isGrappling = false;

        if (lineRenderer != null)
            lineRenderer.positionCount = 0;
    }
}
