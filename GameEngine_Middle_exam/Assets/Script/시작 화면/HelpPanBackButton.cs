using UnityEngine;

public class HelpPanBackButton : MonoBehaviour
{
    public GameObject helpPanel;
    public AudioSource audioSource;
    public AudioClip clickSound;

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
    public void CloseHelp()
    {
        helpPanel.SetActive(false);
    }
}