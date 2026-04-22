using UnityEngine;

public class InvincibleItem : MonoBehaviour
{
    public float duration = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            if (player != null)
            {
                player.ActivateInvincibility(duration);
            }

            Destroy(gameObject);
        }
    }
}