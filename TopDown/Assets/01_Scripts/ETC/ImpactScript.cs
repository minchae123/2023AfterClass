using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactScript : PoolableMono
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void DestroyAfterAnimation()
    {
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
    }

    public virtual void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
        if(audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    public virtual void SetLocalScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
}
