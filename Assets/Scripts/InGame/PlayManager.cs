using Firebase;
using Firebase.Database;
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

    string warningBlank = "공백이 포함되어 있습니다";
    string warningEmpty = "이름을 입력하세요";
    public enum PlayType
    {
        Play,
        Pause,
        End
    }

    [SerializeField] Button pauseButton;
    [SerializeField] Sprite[] pauseImages;
    [SerializeField] Rigidbody2D playerRigid;

    [SerializeField] GameObject gameoverUI;
    string intro = "Intro";
    string scoreString = "운행거리 ";
    [SerializeField] Text scoreText;
    [SerializeField] InputField nameInput;
    [SerializeField] Text warningText;
    string lastName = "lastName";

    int buttonSound = 0;
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
        GameManager.Instance.EffectPlay(buttonSound);
        if (playType == PlayType.Play)
        {
            playType = PlayType.Pause;
            playerRigid.constraints = RigidbodyConstraints2D.FreezeAll;
            GameManager.Instance.BGMPause();
        }
        else if (playType == PlayType.Pause)
        {
            playType = PlayType.Play;
            playerRigid.constraints = RigidbodyConstraints2D.FreezePositionY;
            GameManager.Instance.BGMResume();
        }
        // 정지 상태 변경
        pauseButton.image.sprite = pauseImages[Convert.ToInt32(playType == PlayType.Play)];
        // 정지 버튼 아이콘 변경

        AppOptions options = new AppOptions { DatabaseUrl = new Uri("https://deliverykingthunderbolt-default-rtdb.asia-southeast1.firebasedatabase.app/") };
        FirebaseApp app = FirebaseApp.Create(options);
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("rank");

        RankInfo rankInfo = new RankInfo();
        rankInfo.name = "user_" + ScoreManager.Instance.scoreValue.ToString();
        rankInfo.score = ScoreManager.Instance.scoreValue;

        string jsonString = JsonUtility.ToJson(rankInfo);
        reference.Child(ScoreManager.Instance.scoreValue.ToString()).SetRawJsonValueAsync(jsonString);
    }
    public void GameOver()
    {
        if(GameManager.Instance.getVibe())
            Handheld.Vibrate();

        if (PlayerPrefs.HasKey(lastName))
            nameInput.text = PlayerPrefs.GetString(lastName);

        gameoverUI.SetActive(true);
        scoreText.text = scoreString + string.Format("{0:#,###}m", ScoreManager.Instance.scoreValue);
        // GameOver UI 띄워주고 점수 보여준다
    }
    public void GameRePlay()
    {
        GameManager.Instance.EffectPlay(buttonSound);

        if (nameInput.text.Contains(" "))
        {
            warningText.text = warningBlank;
            warningText.gameObject.SetActive(true);
            return;
        }
        else if (nameInput.text.Equals(string.Empty))
        {
            warningText.text = warningEmpty;
            warningText.gameObject.SetActive(true);
            return;
        }
        else
        {
            ScoreManager.Instance.SaveScore(nameInput.text);
            PlayerPrefs.SetString(lastName, nameInput.text);
        }
        // 이름 입력 받아서 점수 저장
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // InGame Scene 재시작
    }
    public void GoIntro()
    {
        GameManager.Instance.EffectPlay(buttonSound);

        if (nameInput.text.Contains(" "))
        {
            warningText.text = warningBlank;
            warningText.gameObject.SetActive(true);
            return;
        }
        else if (nameInput.text.Equals(string.Empty))
        {
            warningText.text = warningEmpty;
            warningText.gameObject.SetActive(true);
            return;
        }
        else
        {
            ScoreManager.Instance.SaveScore(nameInput.text);
            PlayerPrefs.SetString(lastName, nameInput.text);
        }
        // 이름 입력 받아서 점수 저장
        SceneManager.LoadScene(intro);
        // Intro Scene으로 이동
    }
}
public class RankInfo
{
    public string name;
    public int score;
}
