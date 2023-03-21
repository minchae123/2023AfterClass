using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    protected AudioSource audioSource;
    [SerializeField] protected float pitchRandomness = 0.2f;
    protected float basePitch;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        basePitch = audioSource.pitch;
    }

    public void PlayClipWithVariablePitch(AudioClip clip)
    {
        float randomPitch = Random.Range(-pitchRandomness, pitchRandomness);
        audioSource.pitch = basePitch + randomPitch;
        PlayClip(clip);
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
