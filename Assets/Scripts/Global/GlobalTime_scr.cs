using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GlobalTime_scr : MonoBehaviour {

    public Text ampm;
    public Text minutes;
    public Text hours;
    public string midday;
    public float param;
    public float second1;
    public float minuta1;
    public float hours1;

    private void Start()
    {
        midday = "am";
    }

    void Update () {
        param -= Time.deltaTime;
        if (param <= 0)
        {
            param = 0.015f;
            second1 = second1 + 1;
        }

        if (second1 >= 60)
        {
            minuta1 = minuta1 + 1;
            second1 = 0;
        }

        if (minuta1 >= 60)
        {
            hours1 = hours1 + 1;
            minuta1 = 0;
        }

        if (hours1 > 11)
        {
            hours1 = 1;
            if (midday == "am")
                midday = "pm";
            else
            {
                midday = "am";
            }
        }

        ampm.text = "" + midday;
        minutes.text = "" + minuta1;
        hours.text = "" + hours1 + ":";
    }
}
