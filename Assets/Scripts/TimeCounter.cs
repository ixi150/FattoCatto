using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        StartCoroutine(StartCounting(Time.time));
    }

    IEnumerator StartCounting(float initialTime)
    {
        float time;
        do
        {
            time = Time.time - initialTime;
            text.text = TimeToStringMinSecMillisec(time);
            yield return new WaitForEndOfFrame();
        }
        while (FattoCatto.IsCattoAlive());
        text.text = TimeToStringMinSec(time);
    }

    string TimeToStringMinSecMillisec(float time)
    {
        var span = System.TimeSpan.FromSeconds(time);
        return string.Format("{0}:{1:00}:{2:000}", span.Minutes, span.Seconds, span.Milliseconds);
    }

    string TimeToStringMinSec(float time)
    {
        var span = System.TimeSpan.FromSeconds(time);
        return string.Format("{0}:{1:00}", span.Minutes, span.Seconds);
    }
}
