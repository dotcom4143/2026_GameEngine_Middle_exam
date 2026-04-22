using UnityEngine;

public class MEOWMEOW : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
