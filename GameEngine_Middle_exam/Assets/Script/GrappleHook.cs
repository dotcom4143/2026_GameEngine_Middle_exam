using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public float speed = 7f;
    public float maxDistance = 3f;

    private Vector2 direction;
    private Vector2 startPos;
    private PlayerController player;

    public void Init(Vector2 dir, PlayerController owner)
    {
        direction = dir.normalized;
        player = owner;
        startPos = transform.position;

        Collider2D playerCol = player.GetComponent<Collider2D>();
        Collider2D hookCol = GetComponent<Collider2D>();

        if (playerCol != null && hookCol != null)
        {
            Physics2D.IgnoreCollision(playerCol, hookCol);
        }
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (Vector2.Distance(startPos, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grounded"))
        {
            player.OnHookHit(transform.position);
        }

        Destroy(gameObject);
    }
}
