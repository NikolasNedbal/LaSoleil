using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNight : MonoBehaviour
{
    float secondsInDay = 86400f;

    [SerializeField] Color nightColor;
    [SerializeField] Color dayColor = Color.white;
    [SerializeField] AnimationCurve dayNightCycle;

    [SerializeField] Light2D globLight;

    float timeScale = 1200;
    float time;
    int days;

    [SerializeField] TextMeshProUGUI text;

    private void Update()
    {
        time += Time.deltaTime * timeScale;
        text.text = (time/3600f).ToString();
        float v = dayNightCycle.Evaluate(time/3600);
        Color c = Color.Lerp(dayColor, nightColor, v);
        globLight.color = c;
        if (time > secondsInDay) 
        {
            NewDay();
        }
    }

    private void NewDay()
    {
        time = 0;
        days++;
    }
}
