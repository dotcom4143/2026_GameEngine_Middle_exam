using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectButton : MonoBehaviour
{
    public string ingredientName;
    public string sceneName;

    public AudioSource audioSource;
    public AudioClip clickSound;

    public void SelectStage()
    {
        GameManager.SetIngredient(ingredientName);

        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);

        Invoke(nameof(LoadScene), 0.2f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}