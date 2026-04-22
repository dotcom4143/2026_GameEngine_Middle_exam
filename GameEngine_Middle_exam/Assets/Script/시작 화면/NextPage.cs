using UnityEngine;

public class NextPageButton : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject nextPanel;

    public AudioSource audioSource;
    public AudioClip clickSound;

    public void GoNextPage()
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);

        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
    }
}