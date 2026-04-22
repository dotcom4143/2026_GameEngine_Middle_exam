using UnityEngine;

public class PrevPageButton : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject prevPanel;

    public AudioSource audioSource;
    public AudioClip clickSound;

    public void GoPrevPage()
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);

        currentPanel.SetActive(false);
        prevPanel.SetActive(true);
    }
}