using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    protected AudioSource audionSource;
    [SerializeField]
    protected float pitchRandomness = 0.2f;
    protected float basePitch;

    private void Awake()
    {
        audionSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        basePitch = audionSource.pitch;
    }

    public void PlayClipWithVariablePitch(AudioClip clip)
    {
        float randomPitch = Random.Range(-pitchRandomness, pitchRandomness);
        audionSource.pitch = basePitch + randomPitch;
        PlayClip(clip);
    }

    public void PlayClip(AudioClip clip)
    {
        audionSource.Stop();
        audionSource.clip = clip;
        audionSource.Play();
    }
}
