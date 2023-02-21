using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] float speedValue;
    [SerializeField] RectTransform playerRect;
    [SerializeField] Animator animator;
    
    private int moveIdle = 0;
    private int moveLeft = -1;
    private int moveRight = 1;

    private float currentOil = 100.0f;
    private float oilRatio = 6.9f;
    [SerializeField] RectTransform oilImage;

    private IEnumerator oilCoroutine;

    void Start()
    {
        if (oilCoroutine != null)
            oilCoroutine = null;
        // Coroutine 초기화
        oilCoroutine = OilCoroutine();
        StartCoroutine(oilCoroutine);
    }
    void Update()
    {
        if (PlayManager.Instance.playType != PlayManager.PlayType.Play)
            return;
        // 플레이 중이 아니면 return
        Move();
    }
    // Update 문에서 입력 체크
    void Move()
    {
        if((Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)) 
            || (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)))
            // 왼쪽, 오른쪽 키가 동시에 눌렸거나 둘 다 눌리지 않은 경우
            animator.SetInteger("MoveType", moveIdle);
            // 움직임 없이 Idle 애니메이션 실행
        else if (Input.GetKey(KeyCode.LeftArrow))
        // 왼쪽 키가 눌린 경우
        {
            animator.SetInteger("MoveType", moveLeft);
            playerRect.anchoredPosition -= new Vector2(speedValue, 0.0f);
            // Left 애니메이션 실행, 위치 X값 speedValue 만큼 왼쪽으로 이동
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        // 왼쪽 키가 눌린 경우
        {
            animator.SetInteger("MoveType", moveRight);
            playerRect.anchoredPosition += new Vector2(speedValue, 0.0f);
            // Right 애니메이션 실행, 위치 X값 speedValue 만큼 오른쪽으로 이동
        }
    }
    IEnumerator OilCoroutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1.0f);

            if (currentOil <= 0)
            {
                StopCoroutine(oilCoroutine);
                PlayManager.Instance.playType = PlayManager.PlayType.End;
            }
            // 기름이 0이 되면 Coroutine 종료 PlayType을 End로 설정

            if (PlayManager.Instance.playType == PlayManager.PlayType.Play)
            {
                if(currentOil > 0)
                {
                    --currentOil;
                    oilImage.sizeDelta = new Vector2( currentOil * oilRatio, oilImage.sizeDelta.y);
                }
            }
            // 1.0초 마다 기름이 단다
        }
    }
}
