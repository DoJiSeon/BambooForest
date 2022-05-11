using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_time : MonoBehaviour
{
    public Text timeText;
    public static float time;
    private string memotimes;
    public static float memotime;

    public float LastScore;
    public float BestScore;
    public Text BestScoreText;
    public Text LastScoreText;
    public GameObject GameOver;

    void Start()
    {
        time = 0;
        GameManager.instance.isDie == false;

        BestScore = PlayerPrefs.GetFloat("BestScore", 0);
        LastScore = PlayerPrefs.GetFloat("LastScore", 0);

        BestScoreText.text = BestScore.ToString();
        LastScoreText.text = LastScore.ToString();
    }

    void Update()
    {
        if (GameManager.instance.isDie == false)
        {
            Timer();
        }
        else
        {
            GameOver();
        }

    }

    public  void Timer()
    {
        time += Time.deltaTime;
        timeText.text = string.Format("{0:N2}", time);
        memotimes = string.Format("{0:N2}", time);
        memotime = float.Parse(memotimes); // string to float
    }

    public void Gameover()
    {
        GameOver.SetActive(true);

        BestScore = PlayerPrefs.GetFloat("BestScore", 0);
        LastScore = PlayerPrefs.GetFloat("LastScore", 0);

        if (LastScore == 0)
        {
            LastScore = memotime;
            BestScore = memotime;
            BestScoreText.text = BestScore.ToString();
            LastScoreText.text = LastScore.ToString();

            PlayerPrefs.SetFloat("BestScore", memotime);
            PlayerPrefs.SetFloat("LastScore", memotime);
        }
        else if (times > BestScore)
        {
            LastScore = memotime;
            BestScore = memotime;
            BestScoreText.text = BestScore.ToString();
            LastScoreText.text = LastScore.ToString();

            PlayerPrefs.SetFloat("BestScore", memotime);
            PlayerPrefs.SetFloat("LastScore", memotime);
        }
        else
        {
            LastScore = memotime;
            LastScoreText.text = LastScore.ToString();
            PlayerPrefs.SetFloat("LastScore", times);
        }

    }
}
