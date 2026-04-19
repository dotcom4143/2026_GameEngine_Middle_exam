using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public ElementalChecker elementChecker;

    [Header("Grappler")]
    public GameObject hookPrefab;
    public Transform firePoint;
    public float grappleSpeed = 10f;

    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private LineRenderer lineRenderer;
    private Rigidbody2D rb;
    private Animator pAni;

    private Vector2 grapplePoint;

    private bool isGrounded;
    private bool isGrappling = false;
    private float moveInput;

    private bool isGiant = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer != null)
            lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (elementChecker != null && elementChecker.isNature)
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
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }

        // 이동
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // 방향 & 크기
        if (isGiant)
        {
            if (moveInput < 0)
                transform.localScale = new Vector3(2, 2, 2);
            else if (moveInput > 0)
                transform.localScale = new Vector3(-2, 2, 2);
        }
        else
        {
            if (moveInput < 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (moveInput > 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }

        // (기존 중복 방향 코드 유지)
        if (moveInput < 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput > 0)
            transform.localScale = new Vector3(-1, 1, 1);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveInput = input.x;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            pAni.SetTrigger("Jump");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }

        if (collision.CompareTag("Enemy"))
        {
            if (isGiant)
                Destroy(collision.gameObject);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.CompareTag("Item"))
        {
            isGiant = true;
            Invoke(nameof(ResetGiant), 5f);
            Destroy(collision.gameObject);
        }
    }

    void ResetGiant()
    {
        isGiant = false;
    }

    void FixedUpdate()
    {
        if (isGrappling)
        {
            Vector2 dir = (grapplePoint - (Vector2)transform.position).normalized;
            rb.linearVelocity = dir * grappleSpeed;

            if (Vector2.Distance(transform.position, grapplePoint) < 0.3f)
            {
                StopGrapple();
            }
        }
    }

    void FireHook()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)firePoint.position).normalized;

        GameObject hook = Instantiate(hookPrefab, firePoint.position, Quaternion.identity);
        hook.GetComponent<GrappleHook>().Init(dir, this);
    }

    public void OnHookHit(Vector2 point)
    {
        grapplePoint = point;
        isGrappling = true;

        rb.gravityScale = 0f;

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
        }
    }

    void StopGrapple()
    {
        isGrappling = false;

        rb.gravityScale = 1f;
        rb.linearVelocity = Vector2.zero;

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 0;
        }
    }
}