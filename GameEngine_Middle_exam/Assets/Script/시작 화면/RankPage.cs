using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RankPage : MonoBehaviour
{
    [SerializeField] Transform contentRoot;

    [SerializeField] Transform rowPrefab;

    StageResultList allData;

    void Awake()
    {
        allData = StageResultSaver.LoadRank();
        RefrashRankList();
    }

    void RefrashRankList()
    {
        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }

        var sortedData = allData.results.Where(r => r.stage == 1).OrderByDescending(x => x.score).ToList();

        for (int i = 0; i < sortedData.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, contentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}.{sortedData[i].playerName}-{sortedData[i].score}";
        }
    }




}
