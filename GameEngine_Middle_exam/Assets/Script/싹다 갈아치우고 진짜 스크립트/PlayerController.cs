using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private float moveInput;

    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private bool isGrounded;

    private bool isInvincible = false;

    private float originalSpeed;
    private Coroutine speedCoroutine;
    private Coroutine invincibleCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSpeed = moveSpeed;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        );

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    public void ActivateInvincibility(float duration)
    {
        if (invincibleCoroutine != null)
        {
            StopCoroutine(invincibleCoroutine);
        }

        invincibleCoroutine = StartCoroutine(InvincibleCoroutine(duration));
    }

    IEnumerator InvincibleCoroutine(float duration)
    {
        isInvincible = true;

        yield return new WaitForSeconds(duration);

        isInvincible = false;
    }

    public void ActivateSpeedBoost(float multiplier, float duration)
    {
        if (speedCoroutine != null)
        {
            StopCoroutine(speedCoroutine);
        }

        speedCoroutine = StartCoroutine(SpeedBoostCoroutine(multiplier, duration));
    }

    IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
    {
        moveSpeed = originalSpeed * multiplier;

        yield return new WaitForSeconds(duration);

        moveSpeed = originalSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            if (isInvincible)
            {
                return;
            }

            RestartScene();
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}