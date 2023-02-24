using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    int rankCount = 10;
    string rankString = "rankString";
    string rankInt = "rankInt";
    [SerializeField] Text[] nameArray;  
    [SerializeField] Text[] scoreArray;

    private void Start()
    {
        for (int i = 0; i < rankCount; ++i)
        {
            nameArray[i].text = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(PlayerPrefs.GetString(rankString + i.ToString())));
            scoreArray[i].text = PlayerPrefs.GetInt(rankInt + i.ToString()).ToString();
        }
        // 10등까지 등수별로 이름과 점수 대입
    }
}
