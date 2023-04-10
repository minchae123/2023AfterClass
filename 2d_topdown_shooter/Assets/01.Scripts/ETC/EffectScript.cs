using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EffectScript : PoolableMono
{
    [SerializeField] private float stopTime = 0.5f, lightOffTime = 1f;
    private ParticleSystem particleEffect;
    private Light2D light;
    private float initIntensity;

    private void Awake()
    {
        particleEffect = GetComponent<ParticleSystem>();
        light = transform.Find("Light2D").GetComponent<Light2D>();
        initIntensity = light.intensity;
        light.enabled = false;
    }

    public void PlayEffect()
    {
        particleEffect.Play();
        light.enabled = true;
    }

    public void StopEffect()
    {
        StartCoroutine(DelayOff());
    }

    IEnumerator DelayOff()
    {
        float curTime = 0;
        float remainTime = lightOffTime;
        bool isStop = false;
        while(curTime < lightOffTime)
        {
            curTime += Time.deltaTime;
            remainTime = lightOffTime - curTime;
            if(remainTime < stopTime && isStop == false)
            {
                particleEffect.Stop();
                isStop = true;
            }
            light.intensity = Mathf.Lerp(initIntensity, 0, curTime / lightOffTime);
            yield return null;
        }
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        light.intensity = initIntensity;
    }
}
