using UnityEngine;

public class ObjectMovement : BaseMovement
{
    private float[] xPositionArray = new float[4] { -280.0f, -90.0f, 90.0f, 280.0f };

    private int minValue = 0;
    private int maxValue = 4;

    static int seed;
    System.Random random = new System.Random(seed++);

    public void SetPosition()
    {
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPositionArray[random.Next(minValue, maxValue)], GetStartPosition().y);
        // 난수를 받아서 해당 Index의 X값으로 위치 이동
        this.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayManager.Instance.playType != PlayManager.PlayType.Play)
            return;
        // 플레이 중이 아니면 return

        base.MoveDown();
        if (objectRect.anchoredPosition.y <= GetEndPosition().y)
            ObjectSpawn.Instance.ObjectReturn(this.gameObject);
        // 끝 지점에 도착하면 ObjectPool에 반환
    }
}
