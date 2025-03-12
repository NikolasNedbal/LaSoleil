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

    [SerializeField] Light2D globLight;


    [SerializeField] TextMeshProUGUI text;

    [SerializeField] AnimationCurve dayNightCycle;
    float timeScale = 110.77f;
    float time;
    private void Update()
    {
        time += Time.deltaTime * timeScale;
        text.text = (time / 3600f).ToString("F1");
        float v = dayNightCycle.Evaluate(time / 3600);
        Color c = Color.Lerp(dayColor, nightColor, v);
        globLight.color = c;
        if (time > secondsInDay)
        {
            NewDay();
        }
    }

    public void SkipHour(int numberOfHours)
    {
        time += (3600f * numberOfHours);

        if (time > secondsInDay)
        {
            time -= secondsInDay;
            NewDay();
        }
    }

    public void NewDay()
    {
        time = 0;
        GameManager.Instance.days++;

        if (GameManager.Instance.days > 7)
        {
            GameManager.Instance.days = 1;
            GameManager.Instance.weeks++;
            Rent();
        }

        if (GameManager.Instance.isFerInProcess)
        {
            GameManager.Instance.gameObject.GetComponent<Fermentation>().FermentationProcess();
        }

        GameManager.Instance.GrowPlants();
    }

    public void Rent()
    {
        GameManager.Instance.rent = (150 * GameManager.Instance.weeks);
        Debug.Log("RENT: " + GameManager.Instance.rent);
        GameManager.Instance.money -= GameManager.Instance.rent;
    }
}
