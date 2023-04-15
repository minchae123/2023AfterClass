using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("¿À·ù");
        }
        Instance = this;
    }

    public void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }
}
