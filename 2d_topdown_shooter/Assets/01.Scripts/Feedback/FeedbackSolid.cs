using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackSolid : Feedback
{

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float _solidTime = 0.1f;

    public override void CompleteFeedback()
    {
        StopAllCoroutines();
        spriteRenderer.material.SetInt("_IsSolidColor", 0);
    }

    public override void CreateFeedback()
    {
        if(spriteRenderer.material.HasProperty("_IsSolidColor") )
        {
            spriteRenderer.material.SetInt("_IsSolidColor", 1);
            StartCoroutine(WaitBeforeChagingBack());
        }
    }

    IEnumerator WaitBeforeChagingBack()
    {
        yield return new WaitForSeconds(_solidTime);
        spriteRenderer.material.SetInt("_IsSolidColor", 0);
    }
}
