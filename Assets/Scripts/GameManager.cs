using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Singleton
    [Header("Value")]
    public float speedValue;
    public float accelValue;

    public float maxSpeed = 10.0f;
    private IEnumerator accelCoroutine;

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
    }
    IEnumerator AccelCoroutine()
    {
        while (speedValue < maxSpeed)
        {
            speedValue += accelValue;
            yield return new WaitForSecondsRealtime(1.0f);
            // Realtime으로 1초마다 accelValue를 speedValue에 더해준다
        }
        if (speedValue >= maxSpeed)
            StopCoroutine(accelCoroutine);
            // 최대 속도에 도달하면 Coroutine 종료
    }
}
