using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    public bool isGameOverOpened;

    void Start()
    {
        time = 0;
        GameManager.instance().isDie = false;

        BestScore = PlayerPrefs.GetFloat("BestScore", 0);
        LastScore = PlayerPrefs.GetFloat("LastScore", 0);

        BestScoreText.text = BestScore.ToString();
        LastScoreText.text = LastScore.ToString();

        isGameOverOpened = false;
    }

    void Update()
    {
        if (GameManager.instance().isDie == false)
        {
            Timer();
        }
        else
        {
            if (!isGameOverOpened)
            {
                Gameover();
                isGameOverOpened = true;
            }
        }
        if (isGameOverOpened)
        {
            SceneRestart();
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
        BestScore = PlayerPrefs.GetFloat("BestScore", 0);
        LastScore = PlayerPrefs.GetFloat("LastScore", 0);
        LastScoreText.text = LastScore.ToString();
        if (LastScore == 0)
        {
            LastScore = memotime;
            BestScore = memotime;
            BestScoreText.text = BestScore.ToString();
            LastScoreText.text = LastScore.ToString();

            PlayerPrefs.SetFloat("BestScore", memotime);
            PlayerPrefs.SetFloat("LastScore", memotime);
        }
        else if (memotime > BestScore)
        {
            LastScore = memotime;
            BestScore = memotime;
            BestScoreText.text = LastScore.ToString();
            LastScoreText.text = LastScore.ToString();

            PlayerPrefs.SetFloat("BestScore", memotime);
            PlayerPrefs.SetFloat("LastScore", memotime);
        }
        else
        {
            LastScore = memotime;
            LastScoreText.text = LastScore.ToString();
            PlayerPrefs.SetFloat("LastScore", memotime);
        }
        GameOver.SetActive(true);
    }
    public void SceneRestart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
            isGameOverOpened = false;
        }
    }

    public void sceneRestart_Btn()
    {
        SceneManager.LoadScene(1);
        isGameOverOpened = false;
    }
}
