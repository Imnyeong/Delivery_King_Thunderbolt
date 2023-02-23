using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    string scoreString = "Score : ";
    private IEnumerator scoreCoroutine;

    [SerializeField] Text scoreText;
    [HideInInspector] public int scoreValue = 0;
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
}
