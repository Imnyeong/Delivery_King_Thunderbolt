using Firebase;
using Firebase.Database;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    string settingString = "setting";
    // 0 == 볼륨, 1 == 진동

    [Header("SceneName")]
    string intro = "Intro";
    string inGame = "InGame";

    static int seed;
    System.Random random = new System.Random(seed++);

    int volumeInt = 0;
    int vibeInt = 1;

    int onValue = 1;

    bool volumeBool;
    bool vibeBool;

    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioClip[] bgmClips;

    int bgmIntro = 0;
    int bgmIndex = 1;
    int bgmMax = 5;

    [SerializeField] AudioSource effectSource;
    [SerializeField] AudioClip[] effectClips;

    AppOptions options = new AppOptions { DatabaseUrl = new Uri("") };
    [HideInInspector]
    public DatabaseReference reference;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if (Instance == null)
            Instance = this;
        // Singleton

        if (!PlayerPrefs.HasKey(settingString + volumeInt.ToString()))
            PlayerPrefs.SetInt(settingString + volumeInt.ToString(), onValue);
        if (!PlayerPrefs.HasKey(settingString + vibeInt.ToString()))
            PlayerPrefs.SetInt(settingString + vibeInt.ToString(), onValue);

        volumeBool = Convert.ToBoolean(PlayerPrefs.GetInt(settingString + volumeInt.ToString()));
        vibeBool = Convert.ToBoolean(PlayerPrefs.GetInt(settingString + vibeInt.ToString()));

        FirebaseApp app = FirebaseApp.Create(options);
        reference = FirebaseDatabase.DefaultInstance.GetReference("Rank");
        //reference = FirebaseDatabase.DefaultInstance.RootReference;

        SceneManager.LoadScene(intro);
    }
    #region BGM
    public void BGMPlay()
    {
        if (bgmSource.clip != null)
        {
            bgmSource.Stop();
            bgmSource.clip = null;
        }
        if (!volumeBool)
            return;
        if (SceneManager.GetActiveScene().name == intro)
            bgmSource.clip = bgmClips[bgmIntro];
        else if (SceneManager.GetActiveScene().name == inGame)
            bgmSource.clip = bgmClips[random.Next(bgmIndex, bgmMax)];

        bgmSource.Play();
    }
    public void BGMPause() => bgmSource.Pause();
    public void BGMResume() => bgmSource.Play();
    public void BGMStop()
    {
        bgmSource.Stop();
        bgmSource.clip = null;
    }
    #endregion
    public void EffectPlay(int _index)
    {
        if (!volumeBool)
            return;

        effectSource.clip = effectClips[_index];
        effectSource.Play();
    }
    public void setVolume(bool _isOn)
    {
        volumeBool = _isOn;

        if (!_isOn)
            BGMStop();
        else
            BGMPlay();
    }
    public void setVibe(bool _isOn) => vibeBool = _isOn;
    public bool getVibe()
    {
        return vibeBool;
    }
}
