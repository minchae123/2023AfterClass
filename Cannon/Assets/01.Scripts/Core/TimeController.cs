using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance;

    public void SetTimeScale(float endTimeValue, float timeToWait, Action Oncomplete = null)
    {
        StartCoroutine(TimeScaleCoroutine(endTimeValue, timeToWait, Oncomplete));
    }

    public void StopTimeScale()
    {
        StopAllCoroutines();
        Time.timeScale = 0;
    }

    public void ResetTimeScale()
    {
        StopAllCoroutines();
        Time.timeScale = 1;
    }

    IEnumerator TimeScaleCoroutine(float endTimeValue, float timeToWait, Action Oncomplete = null)
    {
        yield return new WaitForSecondsRealtime(timeToWait);
        Time.timeScale = endTimeValue;
        Oncomplete?.Invoke();
    }
}
