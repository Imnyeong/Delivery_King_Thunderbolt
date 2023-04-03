using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    [SerializeField]
    Text[] nameArray;  
    [SerializeField]
    Text[] scoreArray;

    private List<RankInfo> rankList;
    private int rankCount = 10;

    public void OnEnable()
    {
        rankList = new List<RankInfo>(rankCount);
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
                    Debug.Log(eachInfo["name"] + ", " + eachInfo["score"]);
                }
                SetInformation(rankList);
            }
            else
            {
                Debug.Log("error");
            }
        });
    }
    private void SetInformation(List<RankInfo> list)
    {
        for (int i = 0; i < list.Count; ++i)
        {
            nameArray[i].text = list[i].name;
            scoreArray[i].text = list[i].score.ToString();
        }
    }
}
