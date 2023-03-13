using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;
using DG.Tweening;

public class TorchAnimation : MonoBehaviour
{
    private Light2D light2D;

    private float baseRadius;
    private float baseIntensity;
    private int toggle = 1;

    [SerializeField]
    private float radiusRandomess = 1f;


    private void Awake()
    {
        light2D = GetComponent<Light2D>();
        baseRadius = light2D.pointLightOuterRadius;
        baseIntensity = light2D.intensity;
    }

    private void Start()
    {
        StartShake();
    }

    private void StartShake()
    {
        float targetRadius = baseRadius + Random.Range(0, toggle * radiusRandomess);
        toggle *= -1;

        float targetIntensity = baseIntensity + toggle * Random.Range(0, radiusRandomess * 0.5f);

        float randomTime = Random.Range(0.5f, 0.9f);

        Sequence seq = DOTween.Sequence();

        var t1 = DOTween.To(() => light2D.intensity,
            value => light2D.intensity = value,
            targetIntensity,
            randomTime);

        var t2 = DOTween.To(
            () => light2D.pointLightOuterRadius,
            value => light2D.pointLightOuterRadius = value,
            targetRadius,
            randomTime);

        seq.Append(t1);
        seq.Join(t2);
        seq.AppendCallback(() => StartShake());
    }
}
