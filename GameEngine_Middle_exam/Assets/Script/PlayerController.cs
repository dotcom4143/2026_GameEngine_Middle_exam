using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public ElementalChecker elementChecker;

    [Header("Grappler")]
    public GameObject hookPrefab;
    public Transform firePoint;
    public float grappleSpeed = 5.0f;

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
            lineRenderer.positionCount = 20;

            for (int i = 0; i < 20; i++)
            {
                float t = i / 19f;
                Vector2 point = Vector2.Lerp(transform.position, grapplePoint, t);

                float waveStrength = Mathf.Lerp(0.1f, 0f, t);
                float wave = Mathf.Sin(t * 10f + Time.time * 10f) * waveStrength;

                point += Vector2.Perpendicular(grapplePoint - (Vector2)transform.position).normalized * wave;

                lineRenderer.SetPosition(i, point);
            }
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

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
        // 오류가 많이나서 디버그 확인
        Debug.Log("hookPrefab: " + hookPrefab);
        Debug.Log("firePoint: " + firePoint);
        Debug.Log("camera: " + Camera.main);
        Debug.Log("Shooted");
        Debug.Log(hookPrefab);
        Debug.Log("Ground Hited");

        if (hookPrefab == null || firePoint == null)
        {
            Debug.LogError("HookPrefab or FirePoint not assigned!");
            return;
        }

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
            lineRenderer.positionCount = 20;
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