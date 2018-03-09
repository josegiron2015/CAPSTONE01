using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCycle : MonoBehaviour
{
    float timer = 0.0f;
    int dayCount = 1;
    public int minCount;
    int secCount;
    public int hourCount;
    public Text dayCounter;
    public Text TimeCounter;
    public bool dayReset;

    // Use this for initialization
    void Start()
    {
        dayReset = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.005)
        {
            secCount++;
        }
        if (secCount >= 60)
        {
            minCount++;
            secCount = 0;
        }
        if (minCount >= 60)
        {
            hourCount++;
            minCount = 0;
        }
        if (hourCount > 23)
        {
            dayCount++;
            hourCount = 0;
            minCount = 0;
            secCount = 0;
            dayReset = true;
        }
        //if (hourCount == 0 && dayReset) {
        //dayReset = false;
        //}

        dayCounter.text = dayCount.ToString();
        TimeCounter.text = hourCount.ToString() + "/" + minCount.ToString() + "/" + secCount.ToString();

    }
    public int getDay()
    {
        return dayCount;
    }
    public int getHour()
    {
        return hourCount;
    }
    public bool ifDayReset()
    {
        return dayReset;
    }
    public void NextDay()
    {
        hourCount = 24;
    }
}
