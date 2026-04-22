using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal: MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            SceneManager.LoadScene("Start_openUI");
        }
    }
}