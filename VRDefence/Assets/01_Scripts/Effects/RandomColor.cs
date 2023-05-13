using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomColor : MonoBehaviour
{
    public UnityEvent<Color> OnCreated;
    
    public float arrangeRange = 0.5f;

    private float hueMin = 0, hueMax = 1;
    private float saturationMin = 0.7f, saturationMax = 1;
    private float valueMin = 0.7f, valueMax = 1;

    public void Call()
    {
        var color = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
        OnCreated?.Invoke(color);
    }
}
