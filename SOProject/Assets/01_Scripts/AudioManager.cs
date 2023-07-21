using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
            }
            return instance;
        }
    }

    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    public static void SetMasterVolume(float volume)
    {
        Instance.masterVolume = Mathf.Clamp01(volume);
    }

    public static void SetMusicVolume(float volume)
    {
        Instance.musicVolume = Mathf.Clamp01(volume);
    }

    public static void SetSFXVolume(float volume)
    {
        Instance.sfxVolume = Mathf.Clamp01(volume);
    }
}
