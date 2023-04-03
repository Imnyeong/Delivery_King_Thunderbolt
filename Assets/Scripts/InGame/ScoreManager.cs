using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    string scoreString = "배달거리 : ";

    private IEnumerator scoreCoroutine;

    [SerializeField]
    Text scoreText;
    [HideInInspector] 
    public int scoreValue = 0;
    private List<RankInfo> rankList;
    private int rankCount = 10;

    void Start()
    {
        if (Instance == null)
            Instance = this;
        // Singleton

        //scoreText.text = string.Format("{0:#,###}", scoreValue);
        if (scoreCoroutine != null)
            scoreCoroutine = null;
        // Coroutine 초기화

        scoreCoroutine = ScoreCoroutine();
        StartCoroutine(scoreCoroutine);
    }
    IEnumerator ScoreCoroutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.8f);

            if (PlayManager.Instance.playType == PlayManager.PlayType.Play)
            {
                ++scoreValue;
                scoreText.text = scoreString + string.Format("{0:#,###}m", scoreValue);
            }
            // 일정 시간 마다 점수 획득
        }
    }
    public void SaveScore(string _name)
    {
        rankList = new List<RankInfo>(rankCount);

        //for (int i = 0; i < rankCount; i++)
        //{
        //    RankInfo tmp = new RankInfo();
        //    tmp.name = " ";
        //    tmp.score = 0;
        //    rankList.Add(tmp);
        //}
        //string jsonString = JsonConvert.SerializeObject(rankList);
        //GameManager.Instance.reference.SetRawJsonValueAsync(jsonString);
        //초기화

        GameManager.Instance.reference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot result = task.Result;
        
                foreach (DataSnapshot data in result.Children)
                {
                    IDictionary eachInfo = (IDictionary)data.Value;
                    RankInfo rankInfo = new RankInfo();
                    rankInfo.name = eachInfo["name"].ToString();
                    rankInfo.score = Convert.ToInt32(eachInfo["score"]);
                    rankList.Add(rankInfo);
                    //Debug.Log(eachInfo["name"] + ", " + eachInfo["score"]);
                }
                RankInfo currentInfo = new RankInfo();
                currentInfo.name = _name;
                currentInfo.score = scoreValue;
        
                for (int i = 0; i < rankCount; i++)
                {
                    if (currentInfo.score > rankList[i].score)
                    {
                        for (int j = 9; j > i; j--)
                        {
                            rankList[j] = rankList[j - 1];
                        }
                        rankList[i] = currentInfo;
                        break;
                    }
                }
                string jsonString = JsonConvert.SerializeObject(rankList);
                GameManager.Instance.reference.SetRawJsonValueAsync(jsonString);
            }
            else
            {
                Debug.Log("error");
            }
        });
        //랭킹 교체 알고리즘, 새로 들어온 점수보다 낮으면 위치를 바꾼다
        //입력 받은 이름과 점수 저장
    }
}
