using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float windMoveSpeed = 8f;

    private float currentSpeed;

    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private float moveInput;

    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private bool ground;

    public ElementalChecker elementChecker;

    public Transform blinkTarget;
    public float blinkCooldown = 1f;
    private float blinkTimer;

    // 오류 방지용 (Hook 스크립트 대응)
    public Transform currentHook;
    public bool isHooked;

    public void SetHook(Transform hook)
    {
        currentHook = hook;
        isHooked = true;
    }

    public void ReleaseHook()
    {
        currentHook = null;
        isHooked = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        ground = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        );

        if (Input.GetKeyDown(KeyCode.Space) && ground)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        blinkTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (elementChecker != null && elementChecker.isLight && blinkTimer <= 0f)
            {
                transform.position = blinkTarget.position;
                blinkTimer = blinkCooldown;
            }
        }

        // ?? Wind 속성 처리
        if (elementChecker != null && elementChecker.isWind)
        {
            currentSpeed = windMoveSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);
    }
}