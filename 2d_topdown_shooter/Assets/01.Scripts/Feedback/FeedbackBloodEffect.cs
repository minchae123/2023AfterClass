using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackBloodEffect : Feedback
{
    [SerializeField] private AIActionData data;
    [SerializeField]
    [Range(0f, 1f)]
    private float sizeFactor;

    public override void CompleteFeedback()
    {

    }

    public override void CreateFeedback()
    {
        TextureParticleManager.Instance.SpawnBlood(data.hitPoint, data.hitNormal, sizeFactor);
    }
}
