using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    public static PlayManager Instance;
    // Singleton
    [Header("Value")]
    public float speedValue;
    public float accelValue;

    public float maxSpeed = 10.0f;
    private IEnumerator accelCoroutine;

    public PlayType playType = PlayType.Play;

    public enum PlayType
    {
        Play,
        Pause,
        End
    }

    [SerializeField] Button pauseButton;
    [SerializeField] Sprite[] pauseImages;

    [SerializeField] GameObject gameoverUI;
    string intro = "Intro";
    string scoreString = " 점 달성!";
    [SerializeField] Text scoreText;
    [SerializeField] InputField nameInput;
    [SerializeField] Text warningText;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        // Singleton
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        // 플레이 중에는 화면이 안꺼지도록
        if (accelCoroutine != null)
            accelCoroutine = null;
        // Coroutine 초기화
        accelCoroutine = AccelCoroutine();
        StartCoroutine(accelCoroutine);

        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(OnClickPause);
        // 정지 버튼 리스터 초기화
        GameManager.Instance.BGMPlay();
    }
    IEnumerator AccelCoroutine()
    {
        while (speedValue < maxSpeed)
        {
            yield return new WaitForSecondsRealtime(1.0f);

            if (playType == PlayType.Play)
            {
                speedValue += accelValue;
                // Realtime으로 1초마다 accelValue를 speedValue에 더해준다
            }
        }
        if (speedValue >= maxSpeed || playType == PlayType.End)
            StopCoroutine(accelCoroutine);
        // 최대 속도에 도달하면 Coroutine 종료
    }

    public void OnClickPause()
    {
        //PlayerPrefs.DeleteAll();
        if (playType == PlayType.Play)
        {
            playType = PlayType.Pause;
            GameManager.Instance.BGMPause();
        }
        else if (playType == PlayType.Pause)
        {
            playType = PlayType.Play;
            GameManager.Instance.BGMResume();
        }
        // 정지 상태 변경
        pauseButton.image.sprite = pauseImages[Convert.ToInt32(playType == PlayType.Play)];
        // 정지 버튼 아이콘 변경
        
    }
    public void GameOver()
    {
        gameoverUI.SetActive(true);
        scoreText.text = string.Format("{0:#,###}", ScoreManager.Instance.scoreValue + scoreString);
        // GameOver UI 띄워주고 점수 보여준다
    }
    public void GameRePlay()
    {
        if (nameInput.text.Contains(" "))
        {
            warningText.gameObject.SetActive(true);
            return;
        }
        else
            ScoreManager.Instance.saveScore(nameInput.text);
        // 이름 입력 받아서 점수 저장
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // InGame Scene 재시작
    }
    public void GoIntro()
    {
        if (nameInput.text.Contains(" "))
        {
            warningText.gameObject.SetActive(true);
            return;
        }
        else
            ScoreManager.Instance.saveScore(nameInput.text);
        // 이름 입력 받아서 점수 저장
        SceneManager.LoadScene(intro);
        // Intro Scene으로 이동
    }
}
