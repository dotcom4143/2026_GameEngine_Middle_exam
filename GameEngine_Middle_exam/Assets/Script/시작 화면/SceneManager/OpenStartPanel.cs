using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    public GameObject stagePanel;

    public void Open()
    {
        stagePanel.SetActive(true);
    }
}