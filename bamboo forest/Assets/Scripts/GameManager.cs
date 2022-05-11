using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
          return _instance;
        }
    }
    public float LastScore;
    public float BestScore;
    public float times;
    public bool isDie = false;

    private void Awake()
    {
        BestScore = PlayerPrefs.GetFloat("BestScore", 0);
        LastScore = PlayerPrefs.GetFloat("LastScore", 0);

    }
    void Update()
    {
        if (!isDie)
        {
            Timer();
        }
        else{
            if (LastScore == 0)
            {
                LastScore = times;
                BestScore = times;

                PlayerPrefs.SetFloat("BestScore", times);
                PlayerPrefs.SetFloat("LastScore", times);
            }
            else if (times > BestScore || times == BestScore)
            {
                LastScore = times;
                BestScore = times;
                PlayerPrefs.SetFloat("BestScore", times);
                PlayerPrefs.SetFloat("LastScore", times);
            }
            else
            {
                LastScore = times;
                PlayerPrefs.SetFloat("LastScore", times);
            }
        }
    }

    public void GameoverCheck(bool check)
    {
        isDie = check;
    }

    public void Timer()
    {
        // UI_time 에서 변수 값 받아오기
        times = UI_time.memotime; // float 값 받아옴
    }
}
