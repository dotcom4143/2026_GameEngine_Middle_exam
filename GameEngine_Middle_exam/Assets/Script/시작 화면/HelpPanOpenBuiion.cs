using UnityEngine;

public class HelpPanOpenBuiion : MonoBehaviour
{
    public GameObject helpPanel;

    public void OpenHelp()
    {
        helpPanel.SetActive(true);
    }
    public AudioSource audioSource;
    public AudioClip clickSound;

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}