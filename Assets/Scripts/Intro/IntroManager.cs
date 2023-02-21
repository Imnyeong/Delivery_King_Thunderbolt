﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    string inGame = "InGame";

    public void OnClickStart() 
    { SceneManager.LoadScene(inGame); }
    // 시작 버튼 =>  InGame Scene으로 이동
    public void OnClickRanking() { }
    // 랭킹 버튼 =>
    public void OnClickSetting() { }
    // 설정 버튼 =>
    public void OnClickExit() { Application.Quit(); }
    // 종료 버튼 => 
}