using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_time : MonoBehaviour
{
    public Text timeText;
    private float time;
    void Start()
    {
        
    }


    void Update()
    {
        time += Time.deltaTime;
        timeText.text = string.Format("{0:N2}", time);
    }
}
