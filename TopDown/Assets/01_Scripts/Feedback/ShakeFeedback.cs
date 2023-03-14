using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeFeedback : Feedback
{
    [SerializeField] private Transform objectToShake;
    [SerializeField] private float duration = 0.2f, strength = 1f, randomness = 90f;
    [SerializeField] private int vibrato = 10;
    [SerializeField] private bool snapping = false, fadeOut = true;

    public override void CompleteFeedback()
    {
        objectToShake.DOComplete(); // ������ transform���� �������̴� Ʈ�� ��� ����
    }

    public override void CreateFeedback()
    {
        CompleteFeedback();
        objectToShake.DOShakePosition(duration, strength, vibrato, randomness, snapping, fadeOut);
    }
}
