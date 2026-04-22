using UnityEngine;

public class PlaIWannaExit : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("게임 종료 시도");

        Application.Quit();
    }
}