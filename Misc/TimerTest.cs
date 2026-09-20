using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class TimerTest : MonoBehaviour
{
    private TMP_Text text;
    private float t;
    private int minutes,seconds,rest;
    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {

        float t = Time.timeSinceLevelLoad + GameManager.gameManager.timeHasPassed;

        TimeSpan time = TimeSpan.FromSeconds(t);

       
        if (time.TotalHours >= 1)
        {
            text.text = time.ToString(@"hh\:mm\:ss\:ff");
        }
        else
        {
            text.text = time.ToString(@"mm\:ss\:ff");
        }
    }
}
