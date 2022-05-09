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
    public GameObject GameOver;
    public float LastScore;
    public float BestScore;
    public Text BestScoreText;
    public Text LastScoreText;
    public bool isDie = false;

    public string timeScore;
    public float times;
    private void Awake()
    {
        BestScore = PlayerPrefs.GetFloat("BestScore", 0);
        LastScore = PlayerPrefs.GetFloat("LastScore", 0);

        BestScoreText.text = BestScore.ToString();
        LastScoreText.text = LastScore.ToString();

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
                BestScoreText.text = BestScore.ToString();
                LastScoreText.text = LastScore.ToString();

                PlayerPrefs.SetFloat("BestScore", times);
                PlayerPrefs.SetFloat("LastScore", times);
            }
            else if (times > BestScore)
            {
                LastScore = times;
                BestScore = times;
                BestScoreText.text = BestScore.ToString();
                LastScoreText.text = LastScore.ToString();

                PlayerPrefs.SetFloat("BestScore", times);
                PlayerPrefs.SetFloat("LastScore", times);
            }
            else
            {
                LastScore = times;
                LastScoreText.text = LastScore.ToString();
                PlayerPrefs.SetFloat("LastScore", times);
            }
        }
    }

    public void Gameover()
    {
        GameOver.SetActive(true);
        isDie = true;
    }

    public void Timer()
    {
        // UI_time 에서 변수 값 받아오기
        times = UI_time.memotime;
    }
}
