using UnityEngine;

public class Grap : MonoBehaviour
{
    public float speed = 15f;
    public float maxDistance = 10f;

    private Vector2 direction;
    private Vector2 startPos;

    private Rigidbody2D rb;
    private RealPlayerController player;

    public void Init(Vector2 dir, RealPlayerController owner)
    {
        direction = dir;
        player = owner;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        startPos = transform.position;
        rb.linearVelocity = direction * speed;
    }

    void Update()
    {
        float dist = Vector2.Distance(startPos, transform.position);

        if (dist > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;

            if (player != null)
            {
                player.OnHookHit(transform.position);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
