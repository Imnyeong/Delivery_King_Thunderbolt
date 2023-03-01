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
    int offValue = 0;

    bool volumeBool;
    bool vibeBool;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

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

        SceneManager.LoadScene(intro);
    }
    public void BGMPlay()
    {
        if (audioSource.clip != null)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
        if (!volumeBool)
            return;
        if (SceneManager.GetActiveScene().name == intro)
            audioSource.clip = audioClips[0];
        else if (SceneManager.GetActiveScene().name == inGame)
            audioSource.clip = audioClips[random.Next(1, 6)];

        audioSource.Play();
    }
    public void BGMPause() => audioSource.Pause();
    public void BGMResume() => audioSource.Play();
    public void BGMStop()
    {
        audioSource.Stop();
        audioSource.clip = null;
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
}
