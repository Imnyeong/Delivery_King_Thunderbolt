using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    string scoreString = "Score : ";
    string rankString = "rankString";
    string rankInt = "rankInt";
    private IEnumerator scoreCoroutine;

    [SerializeField] Text scoreText;
    [HideInInspector] public int scoreValue = 0;
    int rankCount = 10;


    void Start()
    {
        if (Instance == null)
            Instance = this;
        // Singleton

        //scoreText.text = string.Format("{0:#,###}", scoreValue);
        if (scoreCoroutine != null)
            scoreCoroutine = null;
        // Coroutine 초기화

        scoreCoroutine = ScoreCoroutine();
        StartCoroutine(scoreCoroutine);
    }

    IEnumerator ScoreCoroutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.8f);

            if (PlayManager.Instance.playType == PlayManager.PlayType.Play)
            {
                ++scoreValue;
                scoreText.text = scoreString + string.Format("{0:#,###}", scoreValue);
            }
            // 일정 시간 마다 점수 획득
        }
    }

    public void saveScore(string _name)
    {
        string convertUserName = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_name));
        for (int i = 0; i < rankCount; i++)
        {
            if (scoreValue > PlayerPrefs.GetInt(rankInt + i.ToString()))
            {
                for (int j = 9; j > i; j--)
                {
                    PlayerPrefs.SetString(rankString + j.ToString(), System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(PlayerPrefs.GetString(rankString + (j - 1).ToString()))));
                    PlayerPrefs.SetInt(rankInt + j.ToString(), PlayerPrefs.GetInt(rankInt + (j - 1).ToString()));
                }
                PlayerPrefs.SetString(rankString + i.ToString(), convertUserName);
                PlayerPrefs.SetInt(rankInt + i.ToString(), scoreValue);
                break;
            }
        }
        // 랭킹 교체 알고리즘, 새로 들어온 점수보다 낮으면 위치를 바꾼다
        // 입력 받은 이름과 점수 저장
    }
}
