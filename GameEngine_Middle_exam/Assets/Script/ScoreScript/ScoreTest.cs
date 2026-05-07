using TMPro;
using UnityEngine;

public class ScoreTest : MonoBehaviour
{

    public TextMeshProUGUI stage1;
    public TextMeshProUGUI stage2;
    public TextMeshProUGUI stage3;
    public TextMeshProUGUI stage4;

    void Start()
    {
        stage1.text = "STAGE 1 : " + HighScore.Load(2).ToString();
        stage2.text = "STAGE 2 : " + HighScore.Load(3).ToString();
        stage3.text = "STAGE 3 : " + HighScore.Load(4).ToString();
        stage4.text = "STAGE 4 : " + HighScore.Load(5).ToString();
    }

}
