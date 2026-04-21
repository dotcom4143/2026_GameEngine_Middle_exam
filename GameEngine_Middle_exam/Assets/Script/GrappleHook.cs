using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    private float speed = 0.5f;
    private float maxDistance = 2f;

    private Vector2 direction;
    private Vector2 startPos;
    private RealPlayerController player;

    public void Init(Vector2 dir, RealPlayerController pc)
    {
        direction = dir;
        player = pc;
        startPos = transform.position;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (Vector2.Distance(startPos, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            player.OnHookHit(transform.position);
            Destroy(gameObject);
        }
    }
}
