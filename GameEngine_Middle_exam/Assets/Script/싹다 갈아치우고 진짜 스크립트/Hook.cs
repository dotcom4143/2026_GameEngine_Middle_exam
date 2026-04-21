using UnityEngine;

public class Hook : MonoBehaviour
{
    public float speed = 20f;
    public float maxDistance = 10f;

    private Rigidbody2D rb;
    private bool isStuck = false;
    private PlayerController player;
    private Vector2 startPos;
    private bool initialized = false;

    public void Init(PlayerController playerController)
    {
        player = playerController;
        startPos = transform.position;
        initialized = true;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.linearVelocity = transform.right * speed;
    }

    void Update()
    {
        if (!initialized || isStuck) return;

        if (Vector2.Distance(startPos, transform.position) >= maxDistance)
        {
            player?.ReleaseHook();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isStuck) return;

        if (collision.gameObject.CompareTag("Ground"))
        {
            isStuck = true;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Static;

            if (player != null)
                player.SetHook(transform);
        }
    }
}