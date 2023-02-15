using System.Collections;
using System.Collections.Generic;
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

    void Update() => Move();
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
}
