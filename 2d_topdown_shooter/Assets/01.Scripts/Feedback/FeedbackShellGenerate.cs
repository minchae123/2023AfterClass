using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackShellGenerate : Feedback
{
    [SerializeField] private Transform shellPosition;

    public override void CompleteFeedback()
    {

    }

    public override void CreateFeedback()
    {
        Vector3 shellPos = shellPosition.position;
        Vector3 dir = shellPosition.up * -1 + shellPosition.forward * -1 * 0.5f;

        TextureParticleManager.Instance.SpawnShell(shellPos, dir);
    }
}
