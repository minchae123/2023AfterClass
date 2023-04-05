using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackTimeFreeze : Feedback
{
    [SerializeField]
    private float freezeTimeDelay = 0.05f, unFreezeTimeDelay = 0.02f;
    [SerializeField]
    [Range(0, 1f)]
    private float timeFreezeValue = 0.2f;

    public override void CompleteFeedback()
    {
        if(TimeController.Instance != null)
            TimeController.Instance.ResetTimeScale();
    }

    public override void CreateFeedback()
    {
        TimeController.Instance?.ModifyTimeScale(timeFreezeValue, freezeTimeDelay, () =>
        {
            TimeController.Instance?.ModifyTimeScale(1, unFreezeTimeDelay);
        });
    }
}
