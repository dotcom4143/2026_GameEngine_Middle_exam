using UnityEngine;

public class UTS : MonoBehaviour
{
    public float multiplier = 2f; 
    public float duration = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            if (player != null)
            {
                player.ActivateSpeedBoost(multiplier, duration);
            }

            Destroy(gameObject);
        }
    }
}