using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEmssionColor : MonoBehaviour
{
    private Renderer target;
    public float emissionIntensity = 5f;

    public void Call( Color color)
    {
        GetComponent<Renderer>().material.SetColor("_EmissionColor", color * emissionIntensity);

    }
}
