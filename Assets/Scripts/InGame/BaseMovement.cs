using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] Vector2 startPosition;
    [SerializeField] Vector2 endPosition;

    public RectTransform objectRect;

    void Update()
    {
        if (!PlayManager.Instance.onPlay)
            return;
        // 플레이 중이 아니면 return
        MoveDown();
    }
    public void MoveDown()
    {
        if (!PlayManager.Instance.onPlay)
            return;
        // 플레이 중이 아니면 return

        if (objectRect.anchoredPosition.y <= endPosition.y)
            objectRect.anchoredPosition = startPosition;  
            // 끝 지점에 도착하면 시작 위치로 초기화
        else
            objectRect.anchoredPosition = new Vector2(objectRect.anchoredPosition.x, objectRect.anchoredPosition.y - PlayManager.Instance.speedValue);
            // 끝 지점에 도착할 때 까지 아래로 이동
    }

    public Vector2 GetStartPosition()
    {
        return startPosition;
    }
    public Vector2 GetEndPosition()
    {
        return endPosition;
    }
}
