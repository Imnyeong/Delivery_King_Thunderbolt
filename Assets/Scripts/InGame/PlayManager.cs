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

    public bool onPlay = true;

    [SerializeField] Button pauseButton;
    [SerializeField] Sprite[] pauseImages;
    
    private void Start()
    {
        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(OnClickPause);

        if (Instance == null)
            Instance = this;
        // Singleton
        if (accelCoroutine != null)
            accelCoroutine = null;
        // Coroutine 초기화
        accelCoroutine = AccelCoroutine();
        StartCoroutine(accelCoroutine);
    }
    IEnumerator AccelCoroutine()
    {
        while (speedValue < maxSpeed)
        {
            {
                speedValue += accelValue;
                // Realtime으로 1초마다 accelValue를 speedValue에 더해준다
            }
            yield return new WaitForSecondsRealtime(1.0f);
        }
        if (speedValue >= maxSpeed)
            StopCoroutine(accelCoroutine);
            // 최대 속도에 도달하면 Coroutine 종료
    }

    public void OnClickPause()
    {
        onPlay = !onPlay;
        pauseButton.image.sprite = pauseImages[Convert.ToInt32(onPlay)];
    }
}
