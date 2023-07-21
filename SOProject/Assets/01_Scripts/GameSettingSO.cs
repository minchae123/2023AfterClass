using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameSettingSO : ScriptableObject
{
    [Header("Audio Setting")]
    public float masterVolum;
    public float musicVolum;
    public float sfxVolum;

    [Header("Graphics Setting")]
    public int resolutionWidth;
    public int resolutionHeight;
    public bool fullScreen;

    [Header("Gameplay Setting")]
    public bool showTutorial;
    public bool enableVibration;
}
