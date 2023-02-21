using System;
using System.Collections;
using UnityEngine;
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
    
    private void Start()
    {
        if (Instance == null)
            Instance = this;
        // Singleton
        if (accelCoroutine != null)
            accelCoroutine = null;
        // Coroutine 초기화
        accelCoroutine = AccelCoroutine();
        StartCoroutine(accelCoroutine);

        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(OnClickPause);
        // 정지 버튼 리스터 초기화
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
        if (playType == PlayType.Play)
            playType = PlayType.Pause;
        else if (playType == PlayType.Pause)
            playType = PlayType.Play;
        // 정지 상태 변경
        pauseButton.image.sprite = pauseImages[Convert.ToInt32(playType == PlayType.Play)];
        // 정지 버튼 아이콘 변경
    }
}
