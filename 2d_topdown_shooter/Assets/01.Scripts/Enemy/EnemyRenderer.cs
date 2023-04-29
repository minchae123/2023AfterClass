using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRenderer : AgentRenderer
{
    private readonly int showRateHash = Shader.PropertyToID("_ShowRate");
    [SerializeField] Vector3 offset;

    private AgentAnimator animator;

    private EffectScript effectScript;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<AgentAnimator>();   
    }

    public void ShowProgress(float time , Action CallBackAction)
    {
        StartCoroutine(ShowCoroutine(time, CallBackAction));
    }

    private IEnumerator ShowCoroutine(float time, Action CallBackAction)
    {
        Material mat = spriteRenderer.material;

        effectScript = PoolManager.Instance.Pop("DustEffect") as EffectScript;
        effectScript.transform.position = transform.position + offset;
        effectScript.PlayEffect(time);

        transform.localPosition = offset;
        float curRate = 1f;
        float percent = 0;
        float curTime = 0;
        animator.SetAnimationSpeed(0);

        while(percent < 1)
        {
            curTime += Time.deltaTime;
            percent = curTime / time;
            curRate = Mathf.Lerp(1, -1, percent);
            mat.SetFloat(showRateHash, curRate);
            transform.localPosition = Vector3.Lerp(offset, Vector3.zero, percent);
            yield return null;
        }
        animator.SetAnimationSpeed(1);
        transform.localPosition = Vector3.zero;
        //effectScript.StopEffect();

        CallBackAction?.Invoke();
    }

    public void Reset()
    {
        StopAllCoroutines();
        animator.SetAnimationSpeed(1);
        spriteRenderer.material.SetFloat(showRateHash, -1f);
        /*if(effectScript != null && effectScript.gameObject.activeSelf)
        {
            effectScript.StopEffect();
        }*/
    }
}
