using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class FeedbackDissolve : Feedback
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float duration = 0.1f;

    public UnityEvent FeedbackComplete;

    public override void CompleteFeedback()
    {
        spriteRenderer.material.SetInt("_IsDissolve", 0);
        spriteRenderer.material.DOComplete();
        spriteRenderer.material.SetFloat("_Dissolve", 1);

    }

    public override void CreateFeedback()
    {
        spriteRenderer.material.SetInt("_IsDissolve", 1);
        spriteRenderer.material.DOFloat(0, "_Dissolve", duration).OnComplete(()=> FeedbackComplete?.Invoke());
    }
}
