using UnityEngine;

public class CocirunParallax : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxEffect = 0.5f;

    private float startPos;
    private float spriteWidth;

    void Start()
    {
        startPos = transform.position.x;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float distance = cameraTransform.position.x * parallaxEffect;
        float movement = cameraTransform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (movement > startPos + spriteWidth)
        {
            startPos += spriteWidth;
        }
        else if (movement < startPos - spriteWidth)
        {
            startPos -= spriteWidth;
        }
    }
}