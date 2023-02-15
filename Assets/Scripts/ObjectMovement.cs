using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] GameManager manager;

    [Header("Value")]
    [SerializeField] Vector2 startPosition;
    [SerializeField] Vector2 endPosition;

    public RectTransform objectRect;

    void Update() => MoveDown();

    public void MoveDown()
    {
        if (objectRect.anchoredPosition.y <= endPosition.y)
            objectRect.anchoredPosition = startPosition;
            // 끝 지점에 도착하면 시작 위치로 초기화
        else
            objectRect.anchoredPosition = new Vector2(startPosition.x, objectRect.anchoredPosition.y - manager.speedValue);
            // 끝 지점에 도착할 때 까지 아래로 이동
    }
}
