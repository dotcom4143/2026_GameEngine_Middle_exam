using UnityEngine;

public class BlinkToHere : MonoBehaviour
{
    public Transform player;
    public Transform mouseFollower;

    public float blinkCooldown = 1.5f;
    private float lastBlinkTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TryBlink();
        }
    }

    void TryBlink()
    {
        if (Time.time < lastBlinkTime + blinkCooldown)
            return;

        if (player == null || mouseFollower == null)
        {
            return;
        }

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.position = mouseFollower.position;
        }
        else
        {
            player.position = mouseFollower.position;
        }

        lastBlinkTime = Time.time;
    }
}