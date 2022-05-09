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
    void Start()
    {
        time = 0;
    }

    void Update()
    {
        //if (GameManager.instance.isDie == false)
        //{
        //    Timer();
        //}
        Timer();
    }
    public  void Timer()
    {
        time += Time.deltaTime;
        timeText.text = string.Format("{0:N2}", time);
        memotimes = string.Format("{0:N2}", time);
        memotime = float.Parse(memotimes);
    }
}
